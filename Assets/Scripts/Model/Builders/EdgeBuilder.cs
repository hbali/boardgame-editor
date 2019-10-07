using Model.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Builders
{
    class EdgeBuilder
    {
        private string mStart;
        private string mEnd;

        public static EdgeBuilder In()
        {
            return new EdgeBuilder();
        }

        public EdgeBuilder SetStart(string start)
        {
            mStart = start;
            return this;
        }

        public EdgeBuilder SetEnd(string end)
        {
            mEnd = end;
            return this;
        }

        public Edge Build()
        {
            Edge edge = BaseModelFactory.CreateInstance<Edge>();
            edge.Start = mStart;
            edge.End = mEnd;
            return edge;
        }
    }
}
