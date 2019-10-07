using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commands;
using Database;
using Extensions;
using Model;
using Model.Builders;
using Model.Repository;
using Networking;
using Newtonsoft.Json;
using Transaction;
using View;

namespace Core
{
    public delegate void NextPlayer(Player next);

    public delegate void PlayerStepped(Player current);
    public delegate void PlayerWon(Player winner);


    public class GameLogic
    {
        private static IBoardGameRepository _repo;

        public static void SetRepo(IBoardGameRepository repo)
        {
            _repo = repo;
        }

        public NextPlayer Next { get; set; }
        public PlayerStepped Stepped { get; set; }
        public PlayerWon Win{ get; set; }

        public List<Player> Players { get; private set; }
        private int counter;
        public Player CurrentPlayer
        {
            get
            {
                return Players[counter];
            }
        }
        public GameLogic()
        {
            Players = _repo.GetAllModelsOfType<Player>().ToList();
            counter = 0;
            NetworkHandler.Instance.ServerMsgReceived += ServerGotMessage;
        }

        private void ServerGotMessage(string msg, GameCommand cmd)
        {
            if(cmd == GameCommand.GameState)
            {
                List<DbPlayer> mementos = JsonConvert.DeserializeObject<List<DbPlayer>>(msg);
                Rebuilder.Changes(mementos.Select(x => x as Memento).ToList()).Build();
            }
        }

        internal void MovePlayer(int dice)
        {
            Field next = CurrentPlayer.CurrentField.JumpToField(dice);
            CurrentPlayer.FieldId = next.Id;
            CommandDispatcher.Instance.Execute<SaveBaseModelsCommand>(CurrentPlayer);
            if (!WinValidate(CurrentPlayer))
            {
                Stepped?.Invoke(CurrentPlayer);
            }
            else
            {
                Win?.Invoke(CurrentPlayer);
            }
        }

        private bool WinValidate(Player currentPlayer) => currentPlayer.CurrencyAmount >= Workspace.Instance.WinAmount;

        public void PlayerLeftOut()
        {
            CurrentPlayer.LeftOut--;
            CommandDispatcher.Instance.Execute<SaveBaseModelsCommand>(CurrentPlayer);
            SwitchPlayers();
        }

        public void SwitchPlayers()
        {
            IncrementCounter();
            Next?.Invoke(CurrentPlayer);
        }

        private void IncrementCounter()
        {
            if(counter < Players.Count - 1)
            {
                counter++;
            }
            else
            {
                counter = 0;
            }
        }

        public void Start()
        {
            Next?.Invoke(CurrentPlayer);
        }
    }
}
