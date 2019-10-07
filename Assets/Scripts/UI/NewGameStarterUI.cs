using System;
using Model;
using Providers.DataProviders;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NewGameStarterUI : UIComponent
    {
        //[SerializeField] SliderWithDisplay slider;
        [SerializeField] Text projectName;
        [SerializeField] Text currencyName;
        [SerializeField] Text currencyAmount;

        public void OnCreateGame()
        {
            if (Validate())
            {
                Workspace.Instance.InitNewGame(projectName.text, currencyName.text, int.Parse(currencyAmount.text));
                UIComponentManager.Instance.AddUIComponent<GameEditorUI>().StartNewGame();
            }
            else
            {
                //popup
            }
        }

        private bool Validate()
        {
            return !string.IsNullOrEmpty(currencyName.text) && !string.IsNullOrEmpty(currencyAmount.text); 
        }

        public override void Dispose()
        {
            projectName.text = "";
            //slider.ResetSlider();
        }
    }
}
