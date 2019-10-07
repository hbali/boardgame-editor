using Model;
using Model.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    class ConnectFieldsCommand : Command
    {
        public override bool Execute(params object[] parameters)
        {
            if (parameters.Length != 2) return false;


            Field start = parameters[0] as Field;
            Field end = parameters[1] as Field;

            Edge edge = EdgeBuilder.In().SetStart(start.Id).SetEnd(end.Id).Build();
            mementos[edge.Id] = edge.GetSnapshot(); 

            Refresh();
            return true;
        }
    }
}
