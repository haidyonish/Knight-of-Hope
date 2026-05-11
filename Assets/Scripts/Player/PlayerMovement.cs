using UnityEngine;

public class PlayerMovement : EntityMovement
{
    [SerializeField] private SoundManager soundManager;

    [Header("Jump")]
    [SerializeField] private float jumpCooldown = 0.12f;
    [SerializeField] private float jumpBufferTime = 0.15f;

    private PlayerStats playerStats;

    private Vector2 moveInput;

    private float nextJumpTime;
    private float jumpBufferCounter;

    protected override void Awake()
    {
        base.Awake();

        playerStats = GetComponent<PlayerStats>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        jumpBufferCounter -= Time.fixedDeltaTime;

        UpdateAnimations();
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void RequestJump()
    {
        jumpBufferCounter = jumpBufferTime;
    }

    protected override void HandleMovement()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(
                moveInput.x * playerStats.MoveSpeed,
                rb.linearVelocity.y
            );
        }

        if ((IsFacingRight && moveInput.x < 0) ||
            (!IsFacingRight && moveInput.x > 0))
        {
            Flip();
        }

        bool canJumpNow =
            jumpBufferCounter > 0f &&
            IsGrounded &&
            canJump &&
            Time.time >= nextJumpTime;

        if (canJumpNow)
        {
            soundManager.PlayJump();

            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                playerStats.JumpForce
            );

            jumpBufferCounter = 0f;

            nextJumpTime = Time.time + jumpCooldown;
        }
    }

    private void UpdateAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
        animator.SetFloat("VelocityY", rb.linearVelocity.y);
        animator.SetBool("IsGrounded", IsGrounded);
    }
}