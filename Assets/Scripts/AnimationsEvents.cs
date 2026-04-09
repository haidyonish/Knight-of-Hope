using UnityEngine;

public class AnimationsEvents : MonoBehaviour
{
    private EntityMovement entityMovement;
    private EntityCombat entityCombat;

    private void Awake()
    {
        entityMovement = GetComponentInParent<EntityMovement>();
        entityCombat = GetComponentInParent<EntityCombat>();
    }

    private void Attack() => entityCombat.DamageTargets();
    private void DisableMovementAndJump() => entityMovement.SetMovementEnabled(false);
    private void EnableMovementAndJump() => entityMovement.SetMovementEnabled(true);
}
