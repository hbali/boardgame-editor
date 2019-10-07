using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction
{
    public interface Memento
    {
        bool Deleted { get; set; }
        string Id { get; set; }
    }
}
