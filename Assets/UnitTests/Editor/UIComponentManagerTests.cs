using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using UI.EventUI;
using UI.Exceptions;
using UnityEngine;

namespace Assets.UnitTests.Editor
{
    [TestFixture]
    public class UIComponentManagerTests
    {
        [Test]
        public void UIComponentManagerThrowsExceptionIfTypeIsntRegistered()
        {
            Assert.Throws<UIComponentNotRegisteredException>(() => UIComponentManager.Instance.AddUIComponent<GameEditorUI>());
        }


        private GameEditorUI ui;
        private GameUI gui;

        [Test]
        public void UIComponentManagerSwitchesComponents()
        {
            ui = new GameObject().AddComponent<GameEditorUI>();
            ui.Awake();
            UIComponentManager.Instance.AddUIComponent<GameEditorUI>();
            Assert.That(UIComponentManager.Instance.Current.GetType() == typeof(GameEditorUI));
        }

        [Test]
        public void AddingUIComponentDisablesCurrentUI()
        {
            ui = new GameObject().AddComponent<GameEditorUI>();
            ui.Awake();
            UIComponentManager.Instance.AddUIComponent<GameEditorUI>();

            gui = new GameObject().AddComponent<GameUI>();
            gui.Awake();
            UIComponentManager.Instance.AddUIComponent<GameUI>();

            Assert.That(!ui.gameObject.activeSelf);
        }

        [Test]
        public void AddingUIPopupComponentDoesntDisableCurrentUI()
        {
            ui = new GameObject().AddComponent<GameEditorUI>();
            ui.Awake();
            UIComponentManager.Instance.AddUIComponent<GameEditorUI>();

            new GameObject().AddComponent<GameEventView>().Awake();
            UIComponentManager.Instance.AddUIComponent<GameEventView>();

            Assert.That(ui.gameObject.activeSelf);
        }

        [TearDown]
        public void Teardown()
        {
            if (ui != null)
            {
                ui.OnDestroy();
                GameObject.DestroyImmediate(ui.gameObject);
            }
            if (gui != null)
            {
                gui.OnDestroy();
                GameObject.DestroyImmediate(gui.gameObject);
            }
        }
    }
}
