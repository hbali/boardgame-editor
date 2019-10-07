using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;

namespace Model
{
    public class GameBoardItem : BaseModel
    {
        public string Description { get; set; }

        public override Memento GetSnapshot(bool deleted = false)
        {
            DbItem item = new DbItem()
            {
                Id = Id,
                description = Description,
                Deleted = deleted
            };
            return item;
        }

        public override void LoadDependantFields()
        {

        }

        public override void LoadModel()
        {

        }

        public override void LoadSnapshot(Memento m)
        {
            DbItem db = m as DbItem;
            Description = db.description;
        }
                
    }
}
