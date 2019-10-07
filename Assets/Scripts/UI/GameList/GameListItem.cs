using Model;
using System;
using Transaction;
using UI.CompactListView;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameList
{
    public class GameListItem : BaseCompactListItem
    {
        private string id;
        private string pName;
        [SerializeField] Text projectName;
        [SerializeField] RectTransform chooser;


        public override void OnClick()
        {
            Workspace.Instance.LoadState(id);

            if (Core.Globals.IsEditor)
            {
                UIComponentManager.Instance.AddUIComponent<GameEditorUI>();
            }
            else
            {
                HandlePlayStates();
            }
        }

        private void HandlePlayStates()
        {
            if (CommandDispatcher.Instance.HasSavedGame(id))
            {
                chooser.gameObject.SetActive(true);
            }
            else
            {
                StartNew();
            }
        }

        private void StartNew()
        {
            if (Core.Globals.SinglePlayer)
            {
                UIComponentManager.Instance.AddUIComponent<GameStarterTutorial>().StartSetup();
            }
            else
            {
                UIComponentManager.Instance.AddUIComponent<GameSync>().SyncGames();
            }
        }

        public void OnLoad()
        {
            Workspace.Instance.LoadSavedGame(id);
            UIComponentManager.Instance.AddUIComponent<GameUI>().Init();
        }

        public void OnNew()
        {
            StartNew();
        }

        internal void Init(string id, string pName)
        {
            this.id = id;
            this.pName = pName;
            projectName.text = pName;
        }
    }
}