using Model;
using Model.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public delegate void PlayerDone(Player p);

    public class PlayerSetup : MonoBehaviour, IDisposable
    {
        [SerializeField] InputField playerName;
        [SerializeField] InputField playerPW;

        private string modelPath;
        private ModelSelectorItem[] modelSelectors;

        private void Awake()
        {
            modelSelectors = GetComponentsInChildren<ModelSelectorItem>();
            foreach (ModelSelectorItem item in modelSelectors)
            {
                item.Clicked += ModelSelected;
            }
            modelSelectors.FirstOrDefault().OnClick();
        }

        public PlayerDone PlayerSet { get; set; }

        private void ModelSelected(string path, ModelSelectorItem selected)
        {
            modelPath = path;
            foreach (ModelSelectorItem item in modelSelectors)
            {
                if(item != selected)
                {
                    item.DeSelect();
                }
            }
        }


        public void OnReady()
        {
            Player current = PlayerBuilder.In()
                .SetName(playerName.text)
                .SetModel(modelPath)
                .SetAntiCheat(playerPW.text)
                .SetField(Workspace.Instance.StartingTile.Field.Id)
                .SetCurrency(Workspace.Instance.CurrentGame.startingCurrency)
                .Build();

            PlayerSet?.Invoke(current);
        }

        internal void Reset()
        {
            playerName.text = "";
            playerPW.text = "";
        }

        public void Dispose()
        {
            foreach (ModelSelectorItem item in modelSelectors)
            {
                item.Clicked -= ModelSelected;
            }
        }
    }
}
