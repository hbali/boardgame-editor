using Core;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.CompactListView;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerInfos
{
    class PlayerInfo : BaseCompactVerticalListView<PlayerInfoItem>
    {
        private const string PrefabPath = "Prefabs/UI/PlayerInfoItem";

        public void Load(IEnumerable<Player> players)
        {
            DestroyAllItems();
            foreach(Player p in players)
            {
                CreateSingleItem(PrefabPath).Set(p);
            }
        }

    }
}
