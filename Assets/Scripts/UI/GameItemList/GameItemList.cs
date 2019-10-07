using Commands;
using Model;
using Model.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;
using UI.CompactListView;

namespace UI.GameItemList
{
    class GameItemList : BaseCompactVerticalListView<GameItemListItem>
    {
        protected override float ItemSize => 40;

        private const string PrefabPath = "Prefabs/UI/GameItemListItem";

        bool changed;
        private List<GameBoardItem> modifiedItems;

        public void LoadList(IEnumerable<GameBoardItem> items)
        {
            modifiedItems = new List<GameBoardItem>();
            foreach (GameBoardItem item in items)
            {
                GameItemListItem gameitem = CreateSingleItem(PrefabPath);
                gameitem.SetItem(item);
                gameitem.Modified += AddToSave;
            }
        }

        private void OnDestroy()
        {
            itemList.ForEach(x => x.Modified -= AddToSave);
        }

        private void AddToSave(GameBoardItem item, string newText)
        {
            modifiedItems.Add(GameBoardItemBuilder.Edit(item).SetDescription(newText).Modify());
            changed = true;
        }
        
        public void OnAddNew()
        {
            GameItemListItem gameitem = CreateSingleItem(PrefabPath);
            GameBoardItem item = GameBoardItemBuilder.In().Build();
            gameitem.SetItem(item);
            gameitem.Modified += AddToSave;
        }

        public void OnSave()
        {
            DestroyAllItems();
            if(changed)
            {
                CommandDispatcher.Instance.Execute<SaveBaseModelsCommand>(modifiedItems);
            }
            UIComponentManager.Instance.RemoveComponent<GameItems>();
        }
    }
}
