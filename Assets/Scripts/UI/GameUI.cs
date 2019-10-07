using Commands;
using Core;
using Model;
using System;
using Transaction;
using UI.EventUI;
using UI.PlayerInfos;
using UnityEngine;

namespace UI
{
    public class GameUI : UIComponent
    {
        [SerializeField] private PlayerInfo pInfos;
        [SerializeField] private AntiCheatUI ac;

        private static System.Random r = new System.Random();
        private GameLogic gl;
        private Player CurrentPlayer { get { return gl.CurrentPlayer; } }

        public override void Dispose()
        {
            gl.Win -= PlayerWin;
            gl.Next -= PlayerMove;
            gl.Stepped -= SteppedPlayer;
            ac.Accepted -= PlayerAccepted;
        }

        internal void Init()
        {
            ac.Accepted += PlayerAccepted;

            gl = new GameLogic();
            pInfos.Load(gl.Players);
            gl.Next += PlayerMove;
            gl.Stepped += SteppedPlayer;
            gl.Win += PlayerWin;
            gl.Start();

        }

        private void PlayerWin(Player winner)
        {
            ShowWinnerUI(winner);
        }

        private void ShowWinnerUI(Player winner)
        {
            UIComponentManager.Instance.AddUIComponent<WinnerUI>().SetWinner(winner);
        }

        public void InitServer()
        {
            gl = new GameLogic();
        }

        private void PlayerMove(Player next)
        {
            AcceptPlayer(next);
        }


        private void SteppedPlayer(Player current) => UIComponentManager.Instance.AddUIComponent<GameEventView>().Set(current);

        private void AcceptPlayer(Player next)
        {
            if(next.LeftOut > 0)
            {
                gl.PlayerLeftOut();
                pInfos.Load(gl.Players);
            }
            else
            {
                AntiCheatUI ac = UIComponentManager.Instance.AddUIComponent<AntiCheatUI>();
                ac.SetPlayer(next);
            }
        }


        public void OnEventView()
        {
            if (!ac.gameObject.activeSelf)
            {
                UIComponentManager.Instance.AddUIComponent<GameEventView>();
            }
        }

        /// <summary>
        /// Right now called from GameEventView footer button
        /// </summary>
        public void EventAccepted()
        {
            CommandDispatcher.Instance.Execute<PlayerEventCommand>(CurrentPlayer);
            pInfos.Load(gl.Players);
            gl.SwitchPlayers();
        }

        /// <summary>
        /// Right now called from AntiCheat footer button
        /// </summary>
        public void PlayerAccepted()
        {
            int dice = r.Next(1, 7);
            ShowDice(dice);
            gl.MovePlayer(dice);
        }

        private void ShowDice(int num)
        {

        }
    }
}
