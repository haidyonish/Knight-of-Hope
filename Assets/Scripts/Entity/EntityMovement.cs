using UnityEngine;

public abstract class EntityMovement : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;
    [SerializeField] protected Transform graphics;

    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.6f, 0.1f);
    [SerializeField] private LayerMask groundLayer;

    protected bool canMove = true;
    protected bool canJump = true;

    public bool IsGrounded { get; private set; }
    public bool IsFacingRight { get; protected set; }

    protected virtual void Awake()
    {
        IsFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        GroundCheck();
        HandleMovement();
    }

    public void SetMovementEnabled(bool enabled)
    {
        rb.linearVelocity = new Vector2(0, 0);
        canMove = enabled;
        canJump = enabled;
    }

    protected abstract void HandleMovement();

    protected void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 scale = graphics.localScale;
        scale.x *= -1;
        graphics.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
            Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
    }

    protected virtual void GroundCheck()
    {
        IsGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0f, groundLayer);
    }
}
