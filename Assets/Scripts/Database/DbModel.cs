using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;

namespace Database
{
    public class DbModel : Memento
    {
        public string Id
        {
            get; set;
        }

        public bool Deleted
        {
            get; set;
        }
    }
}
