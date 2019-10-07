using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;

namespace Model
{
    public interface Snapshot
    {
        Memento GetSnapshot(bool deleted = false);

        void LoadSnapshot(Memento m);
    }
}
