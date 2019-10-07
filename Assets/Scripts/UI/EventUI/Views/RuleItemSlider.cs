using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Base;
using UnityEngine;

namespace UI.EventUI.Views
{
    class RuleItemSlider : RuleItem
    {
        [SerializeField] SliderWithDisplay slider;

        public override object GetRuleVariable()
        {
            return slider.Value;
        }

        public override void SetContent()
        {
            if(rule.obj == Providers.DataProviders.FieldObject.Money)
            {
                slider.Slider.minValue = 1;
                slider.Slider.maxValue = 10000;
            }
            else
            {
                slider.Slider.minValue = 1;
                slider.Slider.maxValue = 6;
            }
        }
    }
}
