using UnityEngine;

public abstract class EntityCombat : MonoBehaviour
{
    protected Animator animator;
    protected EntityMovement movement;

    [SerializeField] protected float attackHeight = 1f;
    [SerializeField] protected float attackOffset = 0f;

    [SerializeField] protected LayerMask targetLayer;

    [Header("Gizmos")]
    [SerializeField] protected bool showAttackGizmo = true;
    [SerializeField] protected Color gizmoColor = Color.red;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<EntityMovement>();
    }

    abstract public void DamageTargets();

    protected Collider2D[] GetTargets(float range)
    {
        int facingDir = movement.IsFacingRight ? 1 : -1;

        Vector2 center = (Vector2)transform.position + new Vector2(facingDir * range / 2f, attackOffset);

        Vector2 size = new Vector2(range, attackHeight);

        return Physics2D.OverlapBoxAll(center, size, 0f, targetLayer);
    }

    protected virtual void OnDrawGizmos()
    {
        if (!showAttackGizmo) return;

        float currentRange = GetAttackRange();
        if (currentRange <= 0) return;

        Gizmos.color = gizmoColor;

        int facingDir = 1;
        if (movement != null)
            facingDir = movement.IsFacingRight ? 1 : -1;
        else if (transform.localScale.x != 0)
            facingDir = transform.localScale.x > 0 ? 1 : -1;

        Vector2 center = (Vector2)transform.position +
                        new Vector2(facingDir * currentRange / 2f, attackOffset);
        Vector2 size = new Vector2(currentRange, attackHeight);

        Gizmos.DrawWireCube(center, size);
    }

    protected virtual float GetAttackRange()
    {
        return 1f;
    }
}