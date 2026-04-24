using UnityEngine;

public class PlayerMovement : EntityMovement
{
    [SerializeField] private SoundManager soundManager;
    private PlayerStats playerStats;

    private bool isJumpPressed;
    private Vector2 moveInput;

    protected override void Awake()
    {
        base.Awake();
        playerStats = GetComponent<PlayerStats>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
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

    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(moveInput.x * playerStats.MoveSpeed, rb.linearVelocity.y);

        if ((IsFacingRight && moveInput.x < 0) || (!IsFacingRight && moveInput.x > 0)) 
            Flip();

        if (isJumpPressed && IsGrounded && canJump)
        {
            soundManager.PlayJump();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerStats.JumpForce);
            isJumpPressed = false;
        }
    }

    private void UpdateAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
        animator.SetFloat("VelocityY", rb.linearVelocity.y);
        animator.SetBool("IsGrounded", IsGrounded);
    }
}
