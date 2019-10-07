using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DbPlayer : DbModel
    {
        //standing on this field
        public string fieldId;
        public int currencyAmount;
        public string model;
        public Dictionary<string, int> items;
        public string playerName;        
        public string anticheat;
        public int leftout;
    }
}
