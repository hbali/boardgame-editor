using Extensions;
using Model;
using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    class PlayerEventCommand : Command
    {
        public override bool Execute(params object[] parameters)
        {
            Player p = parameters[0] as Player;

            if (p == null) return false;
            ApplyEvent(p);
            mementos[p.Id] = p.GetSnapshot();
            Refresh();
            return true;
        }

        private void ApplyEvent(Player p)
        {
            Rule r = p.CurrentField.FEvent.EventRule.First;
            switch (r.obj)
            {
                case FieldObject.Item:
                    {
                        if(p.Items.ContainsKey(r.itemID))
                        {
                            p.Items[r.itemID]++;
                        }
                        else
                        {
                            p.Items.Add(r.itemID, 1);
                        }
                    }
                    break;
                case FieldObject.Money:
                    p.CurrencyAmount += r.amount;
                    break;
                case FieldObject.Round:
                    p.LeftOut = r.amount;
                    break;
                case FieldObject.Step:
                    p.FieldId = p.CurrentField.JumpToField(r.amount).Id;
                    break;

            }
        }
    }
}
