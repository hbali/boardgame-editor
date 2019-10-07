using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Providers.DataProviders
{
    enum PREFS
    {
        SIDECOUNT,
        LASTID
    }

    class UserPreferences
    {
        public static string LastID
        {
            get { return PlayerPrefs.GetString("appdata" + PREFS.LASTID.ToString(), ""); }
            set { PlayerPrefs.SetString("appdata" + PREFS.LASTID.ToString(), value); }
        }
    }
}
