using Model;
using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.GameList;
using UnityEngine;

namespace UI
{
    public class MainMenu : UIComponent
    {
        private void Start()
        {
            UIComponentManager.Instance.DisableAll();
            UIComponentManager.Instance.AddUIComponent<MainMenu>();
        }

        public void OnCreateNewGame()
        {
            UIComponentManager.Instance.AddUIComponent<NewGameStarterUI>();
        }


        public void OnContinue()
        {
            if (!string.IsNullOrEmpty(UserPreferences.LastID))
            {
                Workspace.Instance.LoadLastState();
                UIComponentManager.Instance.AddUIComponent<GameEditorUI>();
            }
            else
            {
                OnCreateNewGame();
            }
        }

        public void OnListGames()
        {
            UIComponentManager.Instance.AddUIComponent<PreviousGamesSelector>().LoadList();
        }

        public override void Dispose()
        {

        }
    }
}
