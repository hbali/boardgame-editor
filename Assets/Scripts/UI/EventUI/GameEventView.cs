using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.EventUI.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UI.EventUI
{
    public class GameEventView : UIPopupComponent
    {
        [SerializeField] private Text eventTitle;
        [SerializeField] private Text eventDescription;
        [SerializeField] private RuleItemText ruleText;
        private FieldEvent evnt;
        private Player player;


        public void OnClose()
        {
            UIComponentManager.Instance.RemoveComponent<GameEventView>();
        }

        public void Set(Player p)
        {
            this.evnt = p.CurrentField.FEvent;
            player = p;
            SetUI();
        }

        private void SetUI()
        {
            eventTitle.text = evnt.EventRule.First.name;
            eventDescription.text = evnt.Text;
            ruleText.SetRule(evnt.EventRule.First);
            ruleText.SetContentByFieldEvent(evnt);
        }

        public void OnOk()
        {
            OnClose();
        }
    }
}
