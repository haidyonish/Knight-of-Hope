using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;

    public bool IsAttacking { get; private set; }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    public void RequestAttack() => TryAttack();

    public void OnLastAttackFrame()
    {
        IsAttacking = false;
    }

    public void DamageTargets()
    {
        Debug.Log("DamageTargets");
    }

    private void TryAttack()
    {
        if (movement.IsGrounded)
        {
            animator.SetTrigger("Attack");
        }
    }
}
