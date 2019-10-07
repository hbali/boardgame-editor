using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour
    {
        public static T Instance
        {
            get; private set;
        }

        protected virtual void Awake()
        {
            Instance = GetComponent<T>();   
        }
    }
}
