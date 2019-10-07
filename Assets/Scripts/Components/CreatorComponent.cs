using Enums;
using GridCreating;
using InputHandling;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TouchScript.Gestures;
using UnityEngine;
using View;

namespace Components
{
    class CreatorComponent : MonoBehaviour
    {
        //for faster main camera access
        private Camera cameraCache;
        private BaseGridTile currentHit;

        private void Start()
        {
            TouchComponent.Instance.TapG += HandleTapGesture;
            cameraCache = Camera.main;
        }

        private void HandleTapGesture(TapGesture gesture)
        {
            LayerMask mask = LayerMask.GetMask(LayerMask.LayerToName((int)LayerMasks.GridTile));
            Ray ray = cameraCache.ScreenPointToRay(new Vector3(gesture.ScreenPosition.x, gesture.ScreenPosition.y, 1));
            RaycastHit hitInfo;
            bool isHit = Physics.Raycast(ray.origin, ray.direction, out hitInfo, float.MaxValue, mask.value);
            
            if (isHit)
            {
                BaseGridTile tile = hitInfo.collider.GetComponent<BaseGridTile>();
                if (tile != null)
                {
                    tile.OnClick();
                }
            }
            else
            {
                GridTileCreator.Instance.HideGhostTiles();
            }

        }
    }
}
