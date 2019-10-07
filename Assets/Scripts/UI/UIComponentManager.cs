using Core;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Exceptions;

namespace UI
{
    public class UIComponentManager : Singleton<UIComponentManager>
    {
        private Dictionary<Type, UIComponent> components;
        private Stack<Type> previousStates;

        private UIComponent current;

        public UIComponent Current { get { return current; } }
        
        public UIComponentManager() : base()
        {
            previousStates = new Stack<Type>();
            components = new Dictionary<Type, UIComponent>();
        }

        internal void DisableAll()
        {
            foreach(UIComponent comp in components.Values)
            {
                comp.gameObject.SetActive(false);
            }
        }

        public void RemoveComponent<T>()
        {
            Type t = typeof(T);
            if(components.ContainsKey(t))
            {
                components[t].gameObject.SetActive(false);
            }
            else
            {
                throw new UIComponentNotRegisteredException("UI component " + t.ToString() + " wasnt registered or doesnt exist");
            }
        }

        public void Register<T>(UIComponent component)
        {
            Register(typeof(T), component);
        }

        public void Register(Type t, UIComponent component)
        {
            components[t] = component; 
        }

        internal void Unregister(Type type)
        {
            components.Remove(type);
        }


        public T AddUIComponent<T>() where T : UIComponent
        {            
            return AddUIComponent(typeof(T)) as T;
        }

        public UIComponent AddUIComponent(Type t)
        {
            if (components.ContainsKey(t))
            {
                if (!t.InheritsFrom(typeof(UIPopupComponent)))
                {
                    SwapComponents(t);
                    return current;
                }
                else
                {
                    components[t].gameObject.SetActive(true);
                    return components[t];
                }
            }
            else
            {
                ///error handling
                ///later we can force instantiate a new prefab based on the type
                throw new UIComponentNotRegisteredException("UI component " + t.ToString() + " wasnt registered or doesnt exist");
            }
        }

        public void ResetLastComponent()
        {
            Type previous = previousStates.Pop();
            AddUIComponent(previous);
        }


        private void SwapComponents(Type t)
        {
            ///animation logic can come here

            ///current is empty when addig the main menu automatically

            if(current != null)
            {
                current.gameObject.SetActive(false);
                previousStates.Push(current.GetType());
                current.Dispose();
            }
            current = components[t];
            current.gameObject.SetActive(true);
        }
    }
}
