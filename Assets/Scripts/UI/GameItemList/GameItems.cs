using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UI.GameItemList
{
    class GameItems : UIPopupComponent
    {
        [SerializeField] private GameItemList gameItemList;

        public void LoadList(IEnumerable<GameBoardItem> items)
        {
            gameItemList.LoadList(items);
        }

        public override void Dispose()
        {

        }
    }
}
