using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    class SaveBaseModelsCommand : Command
    {
        public override bool Execute(params object[] parameters)
        {
            if(parameters.Length != 1)
            {
                return false;
            }
            IEnumerable<BaseModel> models = parameters[0] as IEnumerable<BaseModel>;
            if (models != null)
            {
                foreach (BaseModel model in models)
                {
                    mementos[model.Id] = model.GetSnapshot(model.Deleted);
                }
            }
            else
            {
                BaseModel model = parameters[0] as BaseModel;
                mementos[model.Id] = model.GetSnapshot(model.Deleted);
            }

            Refresh();
            return true;
        }
    }
}
