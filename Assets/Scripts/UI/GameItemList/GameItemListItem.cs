using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using UI.CompactListView;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameItemList
{

    delegate void GameBoardItemModified(GameBoardItem item, string newText);


    /// <summary>
    /// yea the name sounds redundant but it is not
    /// </summary>
    class GameItemListItem : BaseCompactListItem
    {
        private GameBoardItem item;

        [SerializeField] private InputField nameInput;

        public GameBoardItemModified Modified { get; set; }

        public override void OnClick()
        {

        }

        public void OnEditEnd()
        {
            if (!string.IsNullOrEmpty(nameInput.text))
            {
                Modified?.Invoke(item, nameInput.text);
            }
        }

        public void OnDelete()
        {

        }

        internal void SetItem(GameBoardItem item)
        {
            this.item = item;
            nameInput.text = item.Description;
        }
    }
}
