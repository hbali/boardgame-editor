using Model;
using Model.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Builders
{
    class PlayerViewBuilder
    {
        private Player mPlayer;

        public static PlayerViewBuilder In()
        {
            return new PlayerViewBuilder();
        }

        public PlayerViewBuilder SetPlayer(Player player)
        {
            mPlayer = player;
            return this;
        }

        public PlayerView Build()
        {
            PlayerView view = BaseViewModelFactory.CreateModel<PlayerView>(mPlayer.PrefabPath, false);
            view.player = mPlayer;
            view.LoadModel();
            return view;
        }
    }
}
