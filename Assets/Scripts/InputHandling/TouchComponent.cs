using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

namespace InputHandling
{

    public delegate void PanG(ScreenTransformGesture gesture);
    public delegate void TapG(TapGesture gesture);

    public class TouchComponent : SingletonMonoBehaviour<TouchComponent>
    {
        public ScreenTransformGesture TransformGesture;
        public TapGesture TapGesture;

        private void OnEnable()
        {
            TransformGesture.Transformed += PanUpdated;
            TapGesture.Tapped += TapUpdated;
        }

        private void OnDisable()
        {
            TransformGesture.Transformed -= PanUpdated;
            TapGesture.Tapped -= TapUpdated;
        }

        private void TapUpdated(object sender, EventArgs e)
        {
            if (TapG != null)
            {
                TapG.Invoke(sender as TapGesture);
            }
        }

        private void PanUpdated(object sender, EventArgs e)
        {
            if (PanG != null)
            {
                PanG.Invoke(sender as ScreenTransformGesture);
            }
        }
        
        protected override void Awake()
        {
            base.Awake();
        }

        public PanG PanG { get; set; }
        public TapG TapG { get; set; }
    }
}