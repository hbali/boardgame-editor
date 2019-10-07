using Commands;
using Core;
using Database;
using Extensions;
using Model;
using Model.Builders;
using Providers.DataProviders;
using System.Collections.Generic;
using System.Linq;
using Transaction;
using UI;
using UI.EventUI;
using UnityEngine;
using View;
using View.Builders;

namespace GridCreating
{
    class GridTileCreator : Singleton<GridTileCreator>
    {
        protected const float DIST = 1f;
        private Transform tileParent;
        public Transform TileParent
        {
            get
            {
                return tileParent;
            }
        }

        private const string prefabPath = "Prefabs/GridTile";
        private int sideCount;
        GhostGridTile[] ghosts;
        

        private void CreateParent()
        {
            tileParent = new GameObject("Workspace").transform;
        }

        public void StartNewGame()
        {
            ResetGrid();
            CreateStartGridTile(new Vector2(0, 0));
        }

        public void ResetGrid()
        {
            LastFieldId = null;
            if(tileParent != null)
            {
                GameObject.Destroy(tileParent.gameObject);
            }
            CreateParent();
            sideCount = Globals.SideCount;
            ghosts = new GhostGridTile[sideCount];
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i] = GridBuilder<GhostGridTile>.In().SetSides(sideCount).SetParent(tileParent).Build();
                ghosts[i].gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// stores the lastly created field Id
        /// </summary>
        public string LastFieldId { get; set; }

        public void CreateStartGridTile(Vector2 center)
        {
            Rule r = new Rule()
            {
                obj = FieldObject.Money,
                trg = Target.Player,
                src = Source.Bank,
                amount = 10000
            };
            FieldEvent evnt = FieldEventBuilder.In()
                              .SetRules(new RuleChain() { rules = new List<Rule>() { r } })
                              .SetText("You passed start! You get 10000!")
                              .Build();
            CommandDispatcher.Instance.Execute<AddNewGridTileCommand>(center, sideCount, evnt, FieldType.Start);
        }

        private Vector2 cacheCenter;

        public void CreateGridTileWithPopup(Vector2 center)
        {
            cacheCenter = center;
            CommandDispatcher.Instance.Execute<AddNewGridTileCommand>(cacheCenter, sideCount, null, FieldType.Dummy);
        }

        internal void HideGhostTiles()
        {
            EnableGhostTiles(false);
        }

        public void ShowGhostTiles()
        {
            EnableGhostTiles(true);
        }

        private void EnableGhostTiles(bool show)
        {
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].gameObject.SetActive(show);
            }
        }

        internal void ShowNeighbours(Vector2 center, Vector3[] points)
        {
            ShowGhostTiles();
            GhostGridTile ghost;
            Vector2 ghostCenter;
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghost = ghosts[i];

                Vector2 startP = points[i].ToVector2();
                Vector2 dir = startP - center;
                float angle = Mathf.Atan2(dir.y, dir.x);
                ghostCenter = GeometryExtensions.Polar2CartesianRad(startP,
                    angle,
                    DIST/2);
                /*if (i < sideCount - 1)
                {
                    Vector2 startP = GeometryExtensions.Vector2Half(points[i].ToVector2(), points[i + 1].ToVector2());
                    Vector2 dir = startP - center;
                    float angle = Mathf.Atan2(dir.y, dir.x);
                    ghostCenter = GeometryExtensions.Polar2CartesianRad(startP,
                        angle,
                        DIST);
                }
                else
                {
                    Vector2 startP = GeometryExtensions.Vector2Half(points[i].ToVector2(), points[0].ToVector2());
                    Vector2 dir = startP - center;
                    float angle = Mathf.Atan2(dir.y, dir.x);
                    ghostCenter = GeometryExtensions.Polar2CartesianRad(startP,
                        angle,
                        DIST);
                }*/
                GridBuilder<GhostGridTile>.In(ghost).SetCenter(ghostCenter).Modify();
                ghost.GenerateMesh();
            }
        }
    }
}
