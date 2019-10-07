using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class ListExtensions
    {
        public static void ReplaceOrAdd<T>(this List<T> list, T element) where T : DbModel
        {
            int idx = list.FindIndex(0, list.Count, x => x.Id == element.Id);
            if(idx >= 0)
            {
                list[idx] = element;
            }
            else
            {
                list.Add(element);
            }
        }
    }
}
