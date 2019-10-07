using Model;
using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.CompactListView;
using UnityEngine;
using UnityEngine.UI;

namespace UI.EventUI.Views
{
    abstract class RuleItem : BaseCompactListItem
    {
        public const string folder = "Prefabs/EventViews/RuleItems/";

        public static string PrefabPath(FieldObject obj)
        {
            switch (obj)
            {
                case FieldObject.Item:
                    return folder + "RuleDropdown";
                case FieldObject.Money:
                    return folder + "RuleSlider";
                case FieldObject.Round:
                    return folder + "RuleSlider";
                case FieldObject.Step:
                    return folder + "RuleSlider";
                default:
                    return folder + "RuleSlider";
            }
        }

        protected Rule rule;


        public virtual void SetRule(Rule rule)
        {
            this.rule = rule;
        }



        public override void OnClick()
        {
        }
        public abstract void SetContent();

        public abstract object GetRuleVariable();
    }
}
