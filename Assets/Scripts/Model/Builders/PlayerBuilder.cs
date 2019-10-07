using Model.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Builders
{
    class PlayerBuilder
    {
        private string mField;
        private int mAmount;
        private Dictionary<string, int> mItems;
        private string mName;
        private string mModelPath;
        private string mAc;

        public static PlayerBuilder In()
        {
            return new PlayerBuilder();
        }


        public PlayerBuilder SetAntiCheat(string ac)
        {
            mAc = ac;
            return this;
        }

        public PlayerBuilder SetName(string name)
        {
            mName = name;
            return this;
        }

        public PlayerBuilder SetModel(string model)
        {
            mModelPath = model;
            return this;
        }

        public PlayerBuilder SetItems(Dictionary<string, int> items)
        {
            mItems = items;
            return this;
        }

        public PlayerBuilder SetCurrency(int amount)
        {
            mAmount = amount;
            return this;
        }

        public PlayerBuilder SetField(string fieldId)
        {
            mField = fieldId;
            return this;
        }

        public Player Build()
        {
            Player p = BaseModelFactory.CreateInstance<Player>();
            p.CurrencyAmount = mAmount;
            p.Items = mItems;
            p.FieldId = mField;
            p.PlayerName = mName;
            p.ModelPath = mModelPath;
            p.AntiCheat = mAc;
            return p;
        }
    }
}
