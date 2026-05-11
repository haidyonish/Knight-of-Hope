using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : EntityMovement
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float playerPriorityMultiplier = 3f;
    private Transform player;
    private Transform vip;

    public void SetTargets(Transform player, Transform vip)
    {
        this.player = player;
        this.vip = vip;
    }

    public void ApplySpeedMultiplier(float multiplier)
    {
        moveSpeed *= multiplier;
    }

    protected override void HandleMovement()
    {
        if (!canMove)
            return;

        Transform target;

        if (Mathf.Abs(transform.position.x - player.position.x) * playerPriorityMultiplier <
            Mathf.Abs(transform.position.x - vip.position.x))
            target = player;
        else
            target = vip;

        int direction = (transform.position.x < target.position.x) ? 1 : -1;

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        if ((IsFacingRight && direction < 0) || (!IsFacingRight && direction > 0))
            Flip();
    }
}