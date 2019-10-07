using BoardGameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class BoardGameExtensions
    {
        public static bool IsFinished(this BoardGame bg)
        {
            return !bg.fields.Any(x => x.fieldEvent == null) && !bg.edges.Any(x => string.IsNullOrEmpty(x.end) || string.IsNullOrEmpty(x.start));
        }
    }
}
