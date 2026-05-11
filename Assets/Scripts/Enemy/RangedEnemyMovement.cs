using UnityEngine;

public class RangedEnemyMovement : EntityMovement
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float preferredDistance = 8f;

    private Transform vip;

    public bool IsInPosition { get; private set; }

    public void SetTarget(Transform vip)
    {
        this.vip = vip;
    }

    public void ApplySpeedMultiplier(float multiplier)
    {
        moveSpeed *= multiplier;
    }

    protected override void HandleMovement()
    {
        if (!canMove || vip == null)
            return;

        float distance = Mathf.Abs(transform.position.x - vip.position.x);

        IsInPosition = distance <= preferredDistance;

        if (IsInPosition)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        int direction = transform.position.x < vip.position.x ? 1 : -1;

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        if ((IsFacingRight && direction < 0) || (!IsFacingRight && direction > 0))
            Flip();
    }
}