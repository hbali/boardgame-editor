using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Base
{
    public class SliderWithDisplay : MonoBehaviour
    {
        private const int DEFAULT_SLIDER_VALUE = 4;
        [SerializeField] private Text number;
        [SerializeField] private Slider slider;

        public Slider Slider => slider;

        public int Value
        {
            get
            {
                return (int)slider.value;
            }
            set
            {
                slider.value = value;
            }
        }

        internal void ResetSlider()
        {
            Value = DEFAULT_SLIDER_VALUE;
        }

        public void OnValueChanged()
        {
            number.text = slider.value.ToString();
        }
    }
}
