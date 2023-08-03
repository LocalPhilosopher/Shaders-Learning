using System;
using _Code;
using _Code.Player;
using _Code.Sound;
using Code.Utils;
using UnityEngine;

namespace Code.Player.Camera
{
    public class HeadBobController : MonoBehaviour
    {
        [SerializeField] private bool enabled = true;
        [SerializeField] private Vector3 amplitude = new Vector3(.15f,.15f,.15f);
        [SerializeField, Range(0, 30f)] private float frequency = 10.0f;

        [SerializeField] private Transform camera;
        // [SerializeField] private SoundData stepSound;
        private bool left;
        float time;

        private float toggleSpeed = 3.0f;
        private Vector3 startAngles;
        private CharacterController controller;
        private PlayerController player;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            player = GetComponent<PlayerController>();
            startAngles = camera.localEulerAngles;
        }

        void CheckMotion()
        {
            float speed = new Vector3(player.CurrentMoveSpeed.x, 0, player.CurrentMoveSpeed.z).magnitude;
            if (player.IsDead) return;
            if (!controller.isGrounded) return;
            
            PlayMotion(FootStepMotion(Math.Abs(speed / player.MoveSpeed)));
        }
        
        Vector3 FootStepMotion(float speed)
        {
            Vector3 pos = Vector3.zero;
            time += Time.deltaTime * speed;
            pos.x += Mathf.Sin(time * frequency) * amplitude.x * speed;
            pos.y += Mathf.Cos(time * frequency / 2) * amplitude.y * speed;
            pos.z += Mathf.Cos(time * frequency / 2) * amplitude.z * speed;
            if (pos.y > 0 && left)
            {
                left = false;
                // SoundManager.Instance.Play(stepSound);
            }
            else if (pos.y < 0 && !left)
            {
                left = true;
                // SoundManager.Instance.Play(stepSound);
            }
            return pos;
        }

        private void ResetRotation()
        {
            if (camera.localEulerAngles == startAngles) return;
            Vector3 rot = camera.localEulerAngles;
            BasicUnitls.ClampRotation(ref rot);
            camera.localEulerAngles = Vector3.Lerp(rot, startAngles, 5f * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (!enabled) return;
            
            CheckMotion();
            ResetRotation();
        }

        void PlayMotion(Vector3 motion){
            camera.localEulerAngles += motion; 
        }

        
    }
}