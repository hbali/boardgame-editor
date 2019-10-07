using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using UnityEngine;

namespace UI
{
    public class PlayerSelector : UIComponent
    {
        [SerializeField] private RectTransform playerCountSelector;
        [SerializeField] private ServerSelector serverSelector;

        private void Start()
        {
            UIComponentManager.Instance.DisableAll();
            UIComponentManager.Instance.AddUIComponent<PlayerSelector>();
        }


        public void OnThisDevice()
        {
            Globals.SinglePlayer = true;
            UIComponentManager.Instance.AddUIComponent<GameList.PreviousGamesSelector>().LoadList();
        }

        public void OnMultipleDevices()
        {
            Globals.SinglePlayer = false;
            ShowServerSetup();
        }

        private void ShowServerSetup()
        {
            playerCountSelector.gameObject.SetActive(false);
            serverSelector.gameObject.SetActive(true);
        }

        public override void Dispose()
        {
            serverSelector.Dispose();
        }
    }
}
