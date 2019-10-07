using Database;
using Model.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;

namespace Model.Builders
{
    public class Rebuilder
    {
        private List<Memento> mementos;

        public Rebuilder(List<Memento> mementos)
        {
            this.mementos = mementos;
        }

        public static Rebuilder Changes(List<Memento> mementos)
        {
            Rebuilder result = new Rebuilder(mementos);
            return result;
        }

        public void Build()
        {
            BaseModel[] models = mementos.Select(x => BaseModelFactory.LoadInstance(x as DbModel)).ToArray();

            foreach (BaseModel m in models)
            {
                m.LoadDependantFields();
            }
            foreach (BaseModel m in models)
            {
                m.LoadModel();
            }
        }
    }
}
