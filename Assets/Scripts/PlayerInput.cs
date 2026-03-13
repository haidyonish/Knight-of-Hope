using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions input;
    private PlayerMovement movement;
    PlayerCombat combat;

    private void Awake()
    {
        input = new PlayerInputActions();
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;

        input.Player.Jump.started += OnJump;
        input.Player.Attack.started += OnAttack;
    }

    private void OnDisable()
    {
        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;

        input.Player.Jump.started -= OnJump;
        input.Player.Attack.started -= OnAttack;

        input.Disable();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        combat.RequestAttack();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        movement.SetMoveInput(context.ReadValue<Vector2>());
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        movement.RequestJump();
    }
}
