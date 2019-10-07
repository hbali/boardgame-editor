using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace View
{
    public class PlayerView : BaseViewModel
    {
        public Player player { get; set; }

        public override string Id
        {
            get
            {
                return player.Id;
            }
        }

        public override void LoadModel()
        {
            transform.position = new Vector3(player.CurrentField.X, 0, player.CurrentField.Y);
        }
    }
}
