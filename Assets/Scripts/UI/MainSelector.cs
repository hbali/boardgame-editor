using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    class MainSelector : MonoBehaviour
    {
        bool infoOn;
        [SerializeField] private RectTransform infoPanel;

        private void Awake()
        {
            infoOn = false;
        }

        public void OnInfo()
        {
            infoOn = !infoOn;
            ShowInfo(infoOn);
        }

        private void ShowInfo(bool show)
        {
            infoPanel.gameObject.SetActive(show);
        }

        public void OnPlay()
        {
            Core.Globals.IsEditor = false;
            SceneManager.LoadSceneAsync("Game");
        }

        public void OnEdit()
        {
            Core.Globals.IsEditor = true;
            SceneManager.LoadSceneAsync("GameEditor");
        }
    }
}
