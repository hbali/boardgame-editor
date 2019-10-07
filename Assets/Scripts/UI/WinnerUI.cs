using System;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinnerUI : UIPopupComponent
    {
        private const string WINTEXT = "Congratulations for {0}! You won!";
        [SerializeField] private Text winText;

        internal void SetWinner(Player winner)
        {
            winText.text = String.Format(WINTEXT, winner.PlayerName);
        }
    }
}