using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using View;

namespace Model.Factories
{
    public static class BaseViewModelFactory
    {
        private static Dictionary<Type, string> typePathMapping = new Dictionary<Type, string>()
        {
            { typeof(GridTile), "Prefabs/GridTile" },
            { typeof(GhostGridTile), "Prefabs/GridTile" }
        };

        public static T CreateModel<T>(string path = "", bool loadModel = true) where T : BaseViewModel
        {
            if(string.IsNullOrEmpty(path))
            {
                path = typePathMapping[typeof(T)];
            }

            T model = GameObject.Instantiate(Resources.Load<GameObject>(path)).AddIfDontHaveComponent<T>();
            if (loadModel)
            {
                model.LoadModel();
            }
            return model;
        }
               
    }
}
