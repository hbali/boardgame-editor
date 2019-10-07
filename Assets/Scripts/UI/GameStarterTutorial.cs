using Commands;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;
using UI.Base;
using UnityEngine;

namespace UI
{
    public class GameStarterTutorial : UIComponent
    {
        [SerializeField] private SliderWithDisplay slider;
        [SerializeField] private RectTransform countSetter;
        [SerializeField] private PlayerSetup playerSetup;

        private List<Player> createdPlayers;
        private int counter = 0;

        private void Start()
        {
            createdPlayers = new List<Player>();
            playerSetup.PlayerSet += PlayerSet;
        }

        public void OnSetCount()
        {
            countSetter.gameObject.SetActive(false);
            playerSetup.Reset();
            playerSetup.gameObject.SetActive(true);
        }

        private void PlayerSet(Player p)
        {
            if (string.IsNullOrEmpty(p.PlayerName))
            {
                p.PlayerName = "Player #" + counter;
            }
            playerSetup.Reset();
            createdPlayers.Add(p);
            counter++;
            if (counter >= slider.Value)
            {
                GameSet();
            }
        }

        private void GameSet()
        {
            CommandDispatcher.Instance.Execute<SaveBaseModelsCommand>(createdPlayers);
            UIComponentManager.Instance.AddUIComponent<GameUI>().Init();
        }

        public override void Dispose()
        {
            playerSetup.PlayerSet -= PlayerSet;
            createdPlayers.Clear();
        }

        public void StartSetup()
        {
            countSetter.gameObject.SetActive(true);
            playerSetup.gameObject.SetActive(false);
        }
    }
}
