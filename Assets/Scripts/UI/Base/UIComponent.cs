using Core;
using System;
using UnityEngine;

namespace UI
{
    public abstract class UIComponent : MonoBehaviour, IDisposable
    {
        public virtual void Dispose() { }

        public virtual void Awake()
        {
            UIComponentManager.Instance.Register(this.GetType(), this);
        }

        public virtual void OnDestroy()
        {
            UIComponentManager.Instance.Unregister(this.GetType());
        }
    }
}