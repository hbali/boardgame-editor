using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Extensions
{
    public static class GameObjectExtensions
    {
        public static T AddIfDontHaveComponent<T>(this GameObject go) where T : Component
        {
            if (go.GetComponent<T>() == null) return go.AddComponent<T>();
            return go.GetComponent<T>();
        }
    }
}
