using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private PlayerInputActions input;
    private Animator animator;
    [SerializeField] private Transform graphics;


    [SerializeField] private float playerSpeed = 5f;

    private Vector2 moveInput;
    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnDisable()
    {
        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;

        input.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(moveInput.x * playerSpeed, rb.linearVelocity.y);
        if ((isFacingRight && moveInput.x < 0) || (!isFacingRight && moveInput.x > 0)) {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = graphics.localScale;
        scale.x *= -1;
        graphics.localScale = scale;
    }
}
