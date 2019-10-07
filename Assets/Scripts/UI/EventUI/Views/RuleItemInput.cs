using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.EventUI.Views
{
    class RuleItemInput : RuleItem
    {
        [SerializeField] private InputField input;

        public override object GetRuleVariable()
        {
            return input.text;
        }

        public override void SetContent()
        {
        }
    }
}
