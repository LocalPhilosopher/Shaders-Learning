using System;
using Code.Player;
using Code.Player.Camera;
using Code.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code
{
    public class InputHandler : Singleton<InputHandler>
    {
        public enum InputState
        {
            Mobile,
            Keyboard,
            Gamepad
        }

        [SerializeField] private InputState inputState = InputState.Keyboard;
        // [SerializeField] private InputScreen inputScreen;
        [SerializeField] private float mouseSensitivity = 500;
        [SerializeField] private float touchSensitivity = 7.5f;
        private Vector3 previousPosition;

        public InputState CurInputState => inputState;


        public Vector2 GetMoveDirection()
        {
            // if (inputState == InputState.Mobile)
            //     return inputScreen.Joystick.Direction;
            return Vector3.Normalize(new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")));
        }

        public float GetSensitivity()
        {
            if (inputState == InputState.Mobile)
                return touchSensitivity;
            return mouseSensitivity;
        }
        
        public Vector3 GetLookRotation()
        {
            // if (inputState == InputState.Mobile)
            // {
            //     return inputScreen.TouchLookArea.GetLookAxis();
            // }

            Vector3 direction = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            // Debug.Log(direction);
            return direction;
        }
    }
}