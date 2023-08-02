using System;
using _Code;
using _Code.Player;
using Code.Utils;
using UnityEngine;

namespace Code.Player.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform playerBody;
        [SerializeField] private Vector2 angleLimitX;
        float xRotation;
        
        private void LateUpdate()
        {
            if (PlayerController.Instance.IsDead)
                return;
            Vector2 mouseDirection = InputHandler.Instance.GetLookRotation();
            float mouseX = mouseDirection.x * InputHandler.Instance.GetSensitivity() * Time.deltaTime;
            float mouseY = mouseDirection.y * InputHandler.Instance.GetSensitivity() * Time.deltaTime;
        
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, angleLimitX.x, angleLimitX.y);
            transform.localRotation = Quaternion.Euler(xRotation, 0,0);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                LookAt(null, 0);
            }
        }

        public void LookAt(Transform target, float time, Action callback = null)
        {
            // var seq = DOTween.Sequence();
            // seq.Append(transform.DOLookAt(new Vector3(target.position.x, transform.position.y, target.position.z),
            //     time).SetEase(Ease.InBack));
            // seq.Join(playerBody.DOLookAt( new Vector3(target.position.x, playerBody.position.y, target.position.z),
            //     time).SetEase(Ease.InBack));
            // seq.AppendCallback(() =>
            // {
            //     callback?.Invoke();
            // });
        }
        
    }
}