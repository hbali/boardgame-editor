using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;

namespace Model
{
    public class Edge : BaseModel
    {
        public string Start { get; set; }
        public string End { get; set; }

        public Field StartField { get { return _repo.GetModel<Field>(Start); } }
        public Field EndField { get { return _repo.GetModel<Field>(End); } }

        public override Memento GetSnapshot(bool deleted = false)
        {
            return new DbEdge()
            {
                start = Start,
                end = End,
                Id = this.Id,
                Deleted = deleted
            };
        }

        public override void LoadDependantFields()
        {
            if (!string.IsNullOrEmpty(Start))
            {
                StartField.NextEdge = this;
            }
            if (!string.IsNullOrEmpty(End))
            {
                EndField.PrevEdge = this;
            }
        }

        public override void LoadModel()
        {

        }

        public override void LoadSnapshot(Memento m)
        {
            DbEdge edge = m as DbEdge;
            Start = edge.start;
            End = edge.end;
        }
    }
}
