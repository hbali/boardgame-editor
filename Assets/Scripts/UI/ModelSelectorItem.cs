using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UI
{
    class ModelSelectorItem : MonoBehaviour
    {
        [SerializeField] private string path;
        [SerializeField] private RectTransform selected;


        public Action<string, ModelSelectorItem> Clicked { get; internal set; }

        public void OnClick()
        {
            Select();
            Clicked?.Invoke(path, this);
        }

        internal void DeSelect()
        {
            selected.gameObject.SetActive(false);
        }
        
        public void Select()
        {
            selected.gameObject.SetActive(true);
        }
    }
}
