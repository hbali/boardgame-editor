using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Extensions
{
    public static class VectorExtensions
    {

        public static Vector3 ToVector3(this Vector2 v2)
        {
            return new Vector3(v2.x, 0, v2.y);
        }

        public static Vector2 ToVector2(this Vector3 v2)
        {
            return new Vector2(v2.x, v2.z);
        }
    }
}
