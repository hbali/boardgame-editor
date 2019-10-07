using BoardGameModels;
using Core;
using Model.Builders;
using Model.Factories;
using Model.Repository;
using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;
using View;

namespace Model
{
    public class Workspace : Singleton<Workspace>
    {
        private static IBoardGameRepository _repo;

        public BoardGame CurrentGame { get; private set; }

        public int WinAmount
        {
            get
            {
                return (CurrentGame.winningEvent as CurrencyWinningEvent).amount;
            }
        }

        public GridTile StartingTile
        {
            get
            {
                return CalculateStartingTile(CurrentGame);
            }
        }
        public Field LastField
        {
            get
            {
                return _repo.GetAllModelsOfType<Field>().FirstOrDefault(x => x.NextEdge == null);
            }
        }

        public Field FirstField
        {
            get
            {
                return _repo.GetAllModelsOfType<Field>().FirstOrDefault(x => x.PrevEdge == null);
            }
        }

        public IEnumerable<GameBoardItem> Items
        {
            get
            {
                return _repo.GetAllModelsOfType<GameBoardItem>();
            }
        }

        internal void LoadSavedGame(string id)
        {
            SavedGame game = CommandDispatcher.Instance.LoadSavedGame(id);
            Rebuilder.Changes(game.players.Values.Select(x => x as Memento).ToList()).Build();
        }

        internal void InitNewGame(string projectName, string currencyName, int currencyAmount)
        {
            Reset();
            CurrentGame = CommandDispatcher.Instance.CreateNewGame(projectName, currencyName, currencyAmount);
        }

        public void Reset()
        {
            _repo = new BoardGameRepository();
            BaseModel.SetRepo(_repo);
            BaseModelFactory.SetRepo(_repo);
            GameLogic.SetRepo(_repo);
        }

        public void Reset(IBoardGameRepository repo)
        {
            _repo = repo;
            BaseModel.SetRepo(_repo);
            BaseModelFactory.SetRepo(_repo);
            GameLogic.SetRepo(_repo);
        }

        public void CreateFromState(string state)
        {
            Reset();
            CurrentGame = CommandDispatcher.Instance.CreateFromState(state);
            LoadCurrent();
        }

        public void LoadState(string id)
        {
            Reset();
            CurrentGame = CommandDispatcher.Instance.GetState(id);
            LoadCurrent();
        }

        private void LoadCurrent()
        {
            GridCreating.GridTileCreator.Instance.ResetGrid();

            List<Memento> mementos = new List<Memento>(CurrentGame.fields);
            mementos.AddRange(CurrentGame.edges);
            mementos.AddRange(CurrentGame.items);
            mementos.AddRange(CurrentGame.fields.Where(y => y.fieldEvent != null).Select(x => x.fieldEvent));
            Rebuilder.Changes(mementos).Build();

            Globals.CurrencyName = CurrentGame.currency;
            Globals.ItemMapping = CreateItemMapping(CurrentGame);
        }

        private Dictionary<string, string> CreateItemMapping(BoardGame currentGame)
        {
            return _repo.GetAllModelsOfType<GameBoardItem>().ToDictionary(x => x.Id, x => x.Description);
        }

        private GridTile CalculateStartingTile(BoardGame currentGame)
        {
            return _repo.GetModel<Field>(currentGame.fields.Where(x => x.type == Database.FieldType.Start).FirstOrDefault().Id).GridTileView;
        }
        
        internal void LoadLastState()
        {
            LoadState(UserPreferences.LastID);
        }
    }
}
