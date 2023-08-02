using Code;
using Code.Interactive;
using Code.Player.Camera;
using Code.Utils;
using UnityEngine;

namespace _Code.Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        public System.Action OnDead;
        public System.Action<InteractiveObject> OnInteractiveObjectEnter;
        public System.Action<InteractiveObject> OnInteractiveObjectExit;
        [SerializeField] private Transform mouseLook;
        [SerializeField] float moveSpeed;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float jumpHeight = 3;
        [SerializeField] private Vector3 velocity;
        [SerializeField] float groundDistance = .4f;
        [SerializeField] Transform groundCheck;
        [SerializeField] LayerMask groundMask;
        [SerializeField] CameraController cameraController;
        // private InputScreen inputScreen;
        public bool isDead; 

        private Vector3 currentMoveSpeed;
        private CharacterController character;
        private bool isGrounded;
        public Vector3 CurrentMoveSpeed => currentMoveSpeed;
        public float MoveSpeed => moveSpeed;
        public Transform MouseLook => mouseLook;
        public bool IsDead => isDead;

        private void Awake()
        {
            // inputScreen = FindObjectOfType<InputScreen>();
            // OnInteractiveObjectEnter += inputScreen.ShowInteractiveButton;
            // OnInteractiveObjectExit += inputScreen.HideInteractiveButton;
        }

        private void Start()
        {
            character = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Movement();
            // Jump();
            // CheckInteractive();
        }

        private bool hitted = false;
        void CheckInteractive()
        {
            RaycastHit hit;
            UnityEngine.Camera camera = UnityEngine.Camera.main;
            if (UnityEngine.Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, 10))
            {
                Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * hit.distance,
                    Color.yellow);
                if (hit.transform.TryGetComponent<InteractiveObject>(out var interactiveObject))
                {
                    if( hitted)
                        return;
                    hitted = true;
                    OnInteractiveObjectEnter?.Invoke(interactiveObject);
                }
            }
            else
            {
                hitted = false;
                OnInteractiveObjectExit?.Invoke(null);
            }
        }
        
        public void Movement()
        {
            float x;
            float z;

            if (isDead)
                return;
            x = InputHandler.Instance.GetMoveDirection().x;
            z = InputHandler.Instance.GetMoveDirection().y;

            Vector3 move = transform.right * x + transform.forward * z;

            currentMoveSpeed = move * moveSpeed;
            character.Move(currentMoveSpeed * Time.deltaTime);
        }

        public void Alive()
        {
            // GetComponent<CharacterController>().enabled = true;
            isDead = false;
            // inputScreen.gameObject.SetActive(true);
        }

        public void Dead(Transform murder)
        {
            cameraController.LookAt(murder, .1f, () =>
            {
                isDead = true;
                // inputScreen.gameObject.SetActive(false);
                OnDead?.Invoke();
            });
        }
        
        public void Jump()
        {
            isGrounded = UnityEngine.Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
                velocity.y = -2;
            velocity.y += gravity * Time.deltaTime;
            character.Move(velocity * Time.deltaTime);
        }
    }
}