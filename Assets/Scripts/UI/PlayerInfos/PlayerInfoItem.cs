using Core;
using Model;
using UI.CompactListView;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerInfos
{
    internal class PlayerInfoItem : BaseCompactListItem
    {
        [SerializeField] private Text playerName;
        [SerializeField] private Text currencyAmount;
        [SerializeField] private Text leftOut;

        public void Set(Player p)
        {
            playerName.text = p.PlayerName;
            currencyAmount.text = p.CurrencyAmount.ToString() + " " + Globals.CurrencyName;
            if(p.LeftOut > 0)
            {
                leftOut.gameObject.SetActive(true);
                leftOut.text = "(" + p.LeftOut + ")";
            }
            else
            {
                leftOut.gameObject.SetActive(false);
            }
        }

        public override void OnClick()
        {

        }
    }
}