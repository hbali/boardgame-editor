using Extensions;
using Core;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchScript.Gestures;
using UnityEngine;

namespace InputHandling
{
    class CameraHandlerComponent : MonoBehaviour
    {
        private Camera cameraCache;
        private Vector3 scaleCenter;
        private float scaleLength;
        public float panSpeed2D = 1f;
        public float rotateSpeed3D = 0.1f;
        private float degX;
        private float degY;
        Vector3 center = new Vector3(0, 0, 0);
        public float scaleSpeed3D = 20f;

        private void Start()
        {
            cameraCache = Camera.main;
            TouchComponent.Instance.PanG += PanG;
        }

        private void PanG(ScreenTransformGesture gesture)
        {
            if (Core.Globals.IsEditor)
            {
                if (gesture.ActiveTouches.Count >= 2)
                {
                    Move(gesture);
                    Zoom(gesture);
                }
                else
                {
                    Move(gesture);
                }
            }
            else
            {
                if (gesture.ActiveTouches.Count == 1)
                {
                    Rotate3D(gesture);
                }
                else if(gesture.ActiveTouches.Count == 2)
                {
                    Pan3D(gesture);
                    Zoom3D(gesture);
                }
            }
        }

        private void Zoom3D(ScreenTransformGesture gesture)
        {
            var deltaScale = gesture.DeltaScale - 1;
            if (cameraCache.transform.position.y + (cameraCache.transform.forward.y * deltaScale * scaleSpeed3D) < 0)
                return;
            cameraCache.transform.position += (cameraCache.transform.forward * deltaScale * scaleSpeed3D);
        }

        private void Pan3D(ScreenTransformGesture gesture)
        {
            float camDist = Vector3.Distance(cameraCache.transform.position, center);
            float speed = camDist / 1000;
            var delta = gesture.ScreenPosition - gesture.PreviousScreenPosition;
            var translate = (cameraCache.transform.right * -delta.x * speed) + (cameraCache.transform.up * -delta.y * speed);
            if (translate.y + cameraCache.transform.position.y < 0)
                return;
            cameraCache.transform.position += translate;
        }

        private void Rotate3D(ScreenTransformGesture gesture)
        {
            var distance = Vector3.Distance(cameraCache.transform.position, center);

            var delta = gesture.ScreenPosition - gesture.PreviousScreenPosition;
            var deltaX = delta.x * rotateSpeed3D;
            var deltaY = delta.y * rotateSpeed3D;

            degX += deltaX;
            degY += -deltaY;
            degY = degY.ClampAngle(-60f, 90f);

            var rotation = Quaternion.Euler(degY, degX, 0);

            var position = rotation * (Vector3.back * distance) + center;

            if (position.y < 0.5)
            {
                position.y = 0.5f;
            }

            cameraCache.transform.rotation = rotation;
            cameraCache.transform.position = position;
        }

        private void Move(ScreenTransformGesture gesture)
        {
            Vector3 prevPos = cameraCache.ScreenToWorldPoint(gesture.PreviousScreenPosition);
            Vector3 currentPos = cameraCache.ScreenToWorldPoint(gesture.ScreenPosition);
            cameraCache.transform.position -= currentPos - prevPos;
        }

        private void Zoom(ScreenTransformGesture sender)
        {
            float length = Vector2.Distance(sender.ActiveTouches[0].Position, sender.ActiveTouches[1].Position);

            if (sender.PreviousState == Gesture.GestureState.Began)
            {
                scaleLength = length * Camera.main.orthographicSize;
            }
            else if (sender.PreviousState == Gesture.GestureState.Changed)
            {
                cameraCache.orthographicSize = Mathf.Clamp(scaleLength / length, 0.1f, 100);
                float orthoSize = Camera.main.orthographicSize;
            }
        }
    }
}
