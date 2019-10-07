using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;
using View;
using View.Builders;

namespace Model
{
    public class Field : BaseModel
    {
        public FieldType FType { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public FieldEvent FEvent { get; set; }

        private string fieldEventId;
        public string texturePath;

        public Edge PrevEdge { get; set; }

        public Edge NextEdge { get; set; }

        public override BaseViewModel View => base.View;

        public GridTile GridTileView => View as GridTile;

        public bool HasBothEdges
        {
            get
            {
                return PrevEdge != null && NextEdge != null;
            }
        }

        public bool HasEdge
        {
            get
            {
                return PrevEdge != null || NextEdge != null;
            }
        }

        public override Memento GetSnapshot(bool deleted = false)
        {
            DbField field = new DbField()
            {
                x = X,
                y = Y,
                fieldEvent = FEvent?.GetSnapshot() as DbFieldEvent,
                Id = this.Id,
                Deleted = deleted,
                type = FType
            };
            return field;
        }

        public override void LoadSnapshot(Memento m)
        {
            DbField db = m as DbField;
            FType = db.type;
            X = db.x;
            Y = db.y;
            fieldEventId = db.fieldEvent?.Id;
        }

        public override void LoadDependantFields()
        {
            FEvent = _repo.GetModel<FieldEvent>(fieldEventId);
        }

        public override void LoadModel()
        {
            if(GridTileView == null)
            {
                View = GridBuilder<GridTile>.In()
               .SetCenter(new UnityEngine.Vector2(X, Y))
               .SetField(this)
               .SetEvent(FEvent)
               .Build();
            }
            GridTileView.GenerateMesh();
        }
    }
}
