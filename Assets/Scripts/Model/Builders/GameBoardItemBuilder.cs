using Model.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Builders
{
    class GameBoardItemBuilder
    {
        private string mDescription;
        private GameBoardItem mItem;

        public static GameBoardItemBuilder In()
        {
            return new GameBoardItemBuilder();
        }

        public static GameBoardItemBuilder Edit(GameBoardItem item)
        {
            return new GameBoardItemBuilder()
            {
                mItem = item
            };
        }

        public GameBoardItem Modify()
        {
            mItem.Description = mDescription;
            return mItem;
        }

        public GameBoardItemBuilder SetDescription(string descr)
        {
            mDescription = descr;
            return this;
        }

        public GameBoardItem Build()
        {
            GameBoardItem item = BaseModelFactory.CreateInstance<GameBoardItem>();
            item.Description = mDescription;
            return item;
        }
    }
}
