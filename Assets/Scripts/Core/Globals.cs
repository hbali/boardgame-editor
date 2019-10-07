using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Globals
    {
        public static int SideCount { get { return 4; } }

        public static bool SinglePlayer { get; set; }

        public static bool IsServer { get; set; }

        public static bool IsEditor { get; set; }

        public static string CurrencyName { get; set; }

        public static Dictionary<string, string> ItemMapping { get; set; }

    }
}
