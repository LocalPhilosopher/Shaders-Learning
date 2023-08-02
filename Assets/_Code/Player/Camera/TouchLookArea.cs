using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

namespace Code.Player.Camera
{
    public class TouchLookArea : MonoBehaviour, IDragHandler
    {
        private Vector2 lookAxis;
        private float timeCount;
        
        public Vector2 GetLookAxis()
        {
            return lookAxis;
        }
        

        public void OnDrag(PointerEventData data)
        {
            lookAxis = data.delta;
        }

        void ResetAxis()
        {
            if (lookAxis == Vector2.zero) return;
            lookAxis = Vector2.Lerp(lookAxis, Vector2.zero, 12f * Time.deltaTime);
        }
        private void FixedUpdate()
        {
            ResetAxis();
        }
    }
}