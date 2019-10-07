using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;
using UnityEngine;

namespace UI.GameList
{
    class PreviousGamesSelector : UIComponent
    {
        [SerializeField] private GameList games;

        public void LoadList()
        {
            Dictionary<string, ProjectData> projects = CommandDispatcher.Instance.GetProjects();
            games.LoadList(projects);
        }

        public override void Dispose()
        {
            games.Purify();
        }
    }
}
