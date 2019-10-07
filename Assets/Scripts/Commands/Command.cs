using Model.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using Transaction;

namespace Commands
{
    public abstract class Command
    {
        protected internal Dictionary<string, Memento> mementos = new Dictionary<string, Memento>();
        public abstract bool Execute(params object[] parameters);

        protected void Refresh()
        {
            Rebuilder.Changes(mementos.Values.ToList()).Build();
        }
    }
}