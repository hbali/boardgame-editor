using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.EventUI.Views
{
    class RuleItemDropdown : RuleItem
    {
        [SerializeField] private Dropdown dropdown;
        private List<GameBoardItem> items;

        public override object GetRuleVariable()
        {
            return items[dropdown.value].Id;
        }

        public override void SetContent()
        {

        }


        internal void SetGameItems(List<GameBoardItem> items)
        {
            this.items = items;
            List<Dropdown.OptionData> datas = new List<Dropdown.OptionData>();
            foreach(GameBoardItem item in items)
            {
                datas.Add(new Dropdown.OptionData(item.Description));
            }
            dropdown.options = datas;
        }
    }
}
