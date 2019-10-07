using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public enum FieldType
    {
        Standard,
        Start,
        Dummy
    }

    public class DbField : DbModel
    {
        public float x;
        public float y;
        public DbFieldEvent fieldEvent;
        public string texturePath;
        public FieldType type;
    }
}
