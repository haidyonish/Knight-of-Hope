using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;
    private PlayerStats playerStats;
    [SerializeField] private Transform graphics;

    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.6f, 0.1f);
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded { get; private set; }
    private bool canMove = true;
    private bool canJump = true;
    private bool isJumpPressed;

    private Vector2 moveInput;
    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        HandleMovement();
        UpdateAnimations();
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void RequestJump()
    {
        isJumpPressed = true;
    }

    public void SetMovementEnabled(bool enabled)
    {
        canMove = enabled;
        canJump = enabled;
    }

    private void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(moveInput.x * playerStats.MoveSpeed, rb.linearVelocity.y);

        if ((isFacingRight && moveInput.x < 0) || (!isFacingRight && moveInput.x > 0)) 
            Flip();

        if (isJumpPressed && IsGrounded && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerStats.JumpForce);
            isJumpPressed = false;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = graphics.localScale;
        scale.x *= -1;
        graphics.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
            Gizmos.DrawWireCube(groundCheckPoint.position,groundCheckSize);
    }

    private void GroundCheck()
    {
        IsGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0f, groundLayer);
    }

    private void UpdateAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
        animator.SetFloat("VelocityY", rb.linearVelocity.y);
        animator.SetBool("IsGrounded", IsGrounded);
    }
}
