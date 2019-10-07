using Core;
using Model;
using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.EventUI.Views
{
    class RuleItemText : RuleItem, IDisposable
    {
        [SerializeField] Text info;

        public override object GetRuleVariable()
        {
            throw new NotImplementedException("RuleItemText is only for display, doesnt store any value");
        }

        public override void SetContent()
        {
            string txt = rule.src.ToString() + " -> " + rule.obj + " -> " + rule.trg;
            info.text = txt;
        }

        public void Dispose()
        {
            info.text = "";
        }

        public void SetContentByFieldEvent(FieldEvent evnt)
        {
            string txt;
            Rule first = evnt.EventRule.First;
            switch (first.obj)
            {
                case Providers.DataProviders.FieldObject.Item:
                    txt = rule.src.ToString() + " -> " +  Globals.ItemMapping[first.itemID] + " -> " + rule.trg;
                    break;
                case Providers.DataProviders.FieldObject.Money:
                    txt = rule.src.ToString() + " -> " + first.amount + " " + Globals.CurrencyName + " -> " + rule.trg;
                    break;
                case Providers.DataProviders.FieldObject.Round:
                    txt = "You'll be left out from " + first.amount + " rounds";
                    break;
                case Providers.DataProviders.FieldObject.Step:
                    txt = "Step forward " + first.amount + " fields";
                    break;
                default:
                    txt = "";
                    break;
            }
            info.text = txt;
        }
    }
}
