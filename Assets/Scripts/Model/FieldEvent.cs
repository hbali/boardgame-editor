using Database;
using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;

namespace Model
{
    public class FieldEvent : BaseModel
    {
        public string Text { get; set; }
        public RuleChain EventRule { get; set; }

        public override Memento GetSnapshot(bool deleted = false)
        {
            return new DbFieldEvent()
            {
                text = Text,
                eventRule = EventRule,
                Id = this.Id,
                Deleted = deleted
            };
        }

        public override void LoadDependantFields()
        {

        }

        public override void LoadModel()
        {

        }

        public override void LoadSnapshot(Memento m)
        {
            DbFieldEvent db = m as DbFieldEvent;
            Text = db.text;
            Id = db.Id;
            EventRule = db.eventRule;
        }
    }
}
