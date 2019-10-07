using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class FieldExtensions
    {
        public static Field NextField(this Field f)
        {
            return f.NextEdge.EndField;
        }

        public static Field PrevField(this Field f)
        {
            return f.PrevEdge.StartField;
        }

        public static Field JumpToField(this Field f, int jumpAmount)
        {
            for (int i = 0; i < jumpAmount; i++)
            {
                if(f != null)
                {
                    f = NextField(f);
                }
            }
            return f;
        }

    }
}
