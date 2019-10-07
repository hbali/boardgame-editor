using BoardGameModels;
using Database;
using Extensions;
using GridCreating;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI;
using UI.EventUI;
using UnityEngine;

namespace View
{
    public class GridTile : BaseGridTile
    {
        private Material SignMaterial
        {
            get
            {
                switch(Field.FEvent.EventRule.First.obj)
                {
                    case Providers.DataProviders.FieldObject.Item:
                        return Resources.Load<Material>("Materials/GridTileItem");
                    case Providers.DataProviders.FieldObject.Money:
                        return Resources.Load<Material>("Materials/GridTileMoney");
                    case Providers.DataProviders.FieldObject.Round:
                        return Resources.Load<Material>("Materials/GridTileLeftOut");
                    case Providers.DataProviders.FieldObject.Step:
                        return Resources.Load<Material>("Materials/GridTileStep");
                    default:
                        return Resources.Load<Material>("Materials/GridTileMissing");
                }
            }
        }

        protected override Material GridMaterial
        {
            get
            {
                return Resources.Load<Material>("Materials/GridMaterial");
            }
        }

        private void ShowNeighbours()
        {
            GridTileCreator.Instance.ShowNeighbours(Center, points);
        }

        public DbField GetSnapshot()
        {
            return Field.GetSnapshot() as DbField;
        }

        
        public void LoadSnapshot(Field field)
        {
            Center = new Vector2(field.X, field.Y);
        }

        public override void OnClick()
        {
            if(Field.HasBothEdges || (Field.FType == FieldType.Start && Field.HasEdge))
            {
                UIComponentManager.Instance.AddUIComponent<EventTypeChooseView>().Init(Field);
            }
            else
            {
                GridTileCreator.Instance.LastFieldId = Id;
                ShowNeighbours();
            }
        }

        public override void SetSignMaterial()
        {
            if (Field != null && Field.FEvent != null)
            {
                signRenderer.material = SignMaterial;
            }
        }
    }
}
