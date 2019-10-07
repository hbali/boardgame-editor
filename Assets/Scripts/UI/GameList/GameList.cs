using Core;
using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.CompactListView;
using UnityEngine;

namespace UI.GameList
{
    public class GameList : BaseCompactVerticalListView<GameListItem>
    {
        private const string PrefabPath = "Prefabs/GameListItem";
        internal void LoadList(Dictionary<string, ProjectData> projects)
        {
            foreach(KeyValuePair<string, ProjectData> kvp in projects)
            {
                if (kvp.Value.Finished || Globals.IsEditor)
                {
                    GameListItem item = CreateSingleItem(PrefabPath);
                    item.Init(kvp.Key, kvp.Value.Name);
                }
            }
        }

        internal void Purify()
        {
            DestroyAllItems();
        }
    }
}
