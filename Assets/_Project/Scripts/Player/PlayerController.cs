using UnityEngine;
using UnityEngine.InputSystem;
using Game.Managers;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private FloatingJoystick joystick;

        private Rigidbody2D rb;
        private Animator playerAnim;
        private PlayerInput playerInput;
        private InputAction moveAction;
        private SpriteRenderer playerSpriteRenderer;
        public Vector2 inputVec { get; private set; }

        private bool isMobile;
        private bool Running = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            playerAnim = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();
            isMobile = PlatformManager.Instance.IsMobile;
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
            if (joystick != null)
            {
                joystick.gameObject.SetActive(isMobile);
            }
            if (!isMobile)
            {
                moveAction = playerInput.actions["Move"];
            }
        }

        private void Update()
        {
            if (isMobile && joystick != null)
            {
                inputVec = new Vector2(joystick.Horizontal, joystick.Vertical);
            }
            else if (moveAction != null)
            {
                inputVec = moveAction.ReadValue<Vector2>();
            }

            
            if (inputVec.x != 0)
                transform.localScale = new Vector3(Mathf.Sign(inputVec.x), 1, 1);
            if (inputVec.magnitude > 0.1f)
            {
                Running = true;
            }
            else
            {
                Running = false;
            }

            playerAnim.SetBool("Running", Running);

            if (inputVec.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (inputVec.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);

        }

        private void FixedUpdate()
        {
            if (!GameManager.Instance.isGameActive)
                return;

            rb.MovePosition(rb.position + inputVec.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
