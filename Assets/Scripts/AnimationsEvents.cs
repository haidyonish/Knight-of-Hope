using UnityEngine;

public class AnimationsEvents : MonoBehaviour
{
    private EntityMovement entityMovement;
    private EntityCombat entityCombat;
    private RangedEnemyCombat rangedCombat;

    private void Awake()
    {
        entityMovement = GetComponentInParent<EntityMovement>();
        entityCombat = GetComponentInParent<EntityCombat>();
        rangedCombat = GetComponentInParent<RangedEnemyCombat>();
    }

    private void Attack() => entityCombat.DamageTargets();

    private void DisableMovementAndJump() => entityMovement.SetMovementEnabled(false);
    private void EnableMovementAndJump() => entityMovement.SetMovementEnabled(true);

    private void AttackFinished()
    {
        if (entityCombat is EnemyCombat enemy)
        {
            enemy.OnAttackFinished();
        }
    }
    private void ThrowRock()
    {
        rangedCombat?.ThrowRock();
    }
}