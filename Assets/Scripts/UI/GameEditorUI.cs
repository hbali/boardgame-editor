using Commands;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;

namespace UI
{
    public class GameEditorUI : UIComponent
    {
        public void StartNewGame()
        {
            GridCreating.GridTileCreator.Instance.StartNewGame();
        }

        public void OnItems()
        {
            UIComponentManager.Instance.AddUIComponent<GameItemList.GameItems>().LoadList(Workspace.Instance.Items);
        }

        public void OnWinningEvent()
        {

        }

        public void OnConnect()
        {
            Field start = Workspace.Instance.LastField;
            Field end = Workspace.Instance.StartingTile.Field;
            if(start != null && end != null && CommandDispatcher.Instance.Execute<ConnectFieldsCommand>(start, end))
            {

            }
            else
            {

            }
        }

        public void OnQuit()
        {

        }

        public override void Dispose()
        {

        }
    }
}
