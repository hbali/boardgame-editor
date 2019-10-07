using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Handles app start
    /// </summary>
    class BoardGameEditorApplication : MonoBehaviour
    {
        private void Awake()
        {
            ProjectProvider pp = new ProjectProvider();
            CommandDispatcher.Instance.Initialize(pp, Globals.IsEditor ? null : new SaveGameProvider());
        }
    }
}
