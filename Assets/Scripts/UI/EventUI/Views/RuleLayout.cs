using BoardGameModels;
using Database;
using Model;
using Model.Builders;
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
    class RuleLayout : BaseCompactVerticalListView<RuleItem>
    {
        const string RuleTextPath = RuleItem.folder + "RuleText";
        const string RuleInputPath = RuleItem.folder + "RuleInput";

        [SerializeField] private Text title;
        private Rule rule;
        private RuleItem item;
        private RuleItemInput input;

        protected override float ItemSize => 50;

        public void LoadRule(Rule rule)
        {
            this.rule = rule;
            title.text = rule.name;

            CreateInput();
            CreateRuleSetter(rule);
            CreateInfo();
        }

        private void CreateRuleSetter(Rule rule)
        {
            item = CreateSingleItem(RuleItem.PrefabPath(rule.obj));
            item.SetRule(rule);            
            item.SetContent();
            if(item is RuleItemDropdown)
            {
                (item as RuleItemDropdown).SetGameItems(Workspace.Instance.Items.ToList());
            }
        }

        private void CreateInput()
        {
            input = CreateSingleItem(RuleInputPath) as RuleItemInput;
        }

        private void CreateInfo()
        {
            RuleItemText text = CreateSingleItem(RuleTextPath) as RuleItemText;
            text.SetRule(rule);
            text.SetContent();
        }

        public FieldEvent GetFieldEvent()
        {
            if (item is RuleItemDropdown)
            {
                rule.itemID = item.GetRuleVariable() as string;
            }
            else if (item is RuleItemSlider)
            {
                rule.amount = (int)item.GetRuleVariable();
            }

            return FieldEventBuilder.In().SetRules(new RuleChain { rules = new List<Rule> { rule } } )
                                         .SetText(input.GetRuleVariable() as string)
                                         .Build();
        }

        public void Dispose()
        {
            DestroyAllItems();
        }
    }
}
