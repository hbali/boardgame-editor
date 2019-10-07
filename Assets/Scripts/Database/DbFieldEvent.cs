using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DbFieldEvent : DbModel
    {
        public string text;
        public RuleChain eventRule { get; set; }
    }
}
