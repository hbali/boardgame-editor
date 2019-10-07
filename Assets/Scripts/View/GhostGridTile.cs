using GridCreating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace View
{
    class GhostGridTile : BaseGridTile
    {
        protected override Material GridMaterial
        {
            get
            {
                return Resources.Load<Material>("Materials/GhostMaterial");
            }
        }

        public override void OnClick()
        {
            GridTileCreator.Instance.CreateGridTileWithPopup(Center);
            GridTileCreator.Instance.HideGhostTiles();
        }

        public override void ModifyHeight()
        {
            transform.position = new Vector3(transform.position.x, -1, transform.position.z);
        }
    }
}
