using Database;
using GridCreating;
using Model;
using Model.Builders;
using System;
using UnityEngine;
using View;
using View.Builders;

namespace Commands
{
    class AddNewGridTileCommand : Command
    {
        public override bool Execute(params object[] parameters)
        {
            try
            {
                if (parameters.Length != 4) return false;

                Vector2 center = (Vector2)parameters[0];
                int sideCount = (int)parameters[1];
                FieldEvent evnt = parameters[2] as FieldEvent;
                FieldType type = (FieldType)parameters[3];

                GridTile tile = GridBuilder<GridTile>.In()
                .SetCenter(center)
                .SetSides(sideCount)
                .SetEvent(evnt)
                .SetType(type)
                .Build();

                tile.GenerateMesh();

                if(!string.IsNullOrEmpty(GridTileCreator.Instance.LastFieldId))
                {
                    Edge edge = EdgeBuilder.In()
                        .SetStart(GridTileCreator.Instance.LastFieldId)
                        .SetEnd(tile.Id)
                        .Build();

                    mementos[edge.Id] = edge.GetSnapshot();
                }

                if (evnt != null)
                {
                    mementos[evnt.Id] = evnt.GetSnapshot();
                }
                mementos[tile.Id] = tile.GetSnapshot();

                Refresh();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
