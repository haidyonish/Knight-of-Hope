using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class AnimationsEvents : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerCombat = GetComponentInParent<PlayerCombat>();
    }

    private void Attack() => playerCombat.DamageTargets();
    private void DisableMovementAndJump() => playerMovement.SetMovementEnabled(false);
    private void EnableMovementAndJump() => playerMovement.SetMovementEnabled(true);
}
