using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions input;
    private PlayerMovement movement;
    [SerializeField] private GameManager gameManager;
    private PlayerCombat combat;

    private bool _inputEnabled = true;

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

    private void Update()
    {
        if (!_inputEnabled)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            gameManager?.TogglePause();
        }
    }

    private void OnDisable()
    {
        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;

        input.Player.Jump.started -= OnJump;
        input.Player.Attack.started -= OnAttack;

        input.Disable();
    }

    public void EnableInput()
    {
        _inputEnabled = true;

        Vector2 currentMove =
            input.Player.Move.ReadValue<Vector2>();

        movement.SetMoveInput(currentMove);
    }

    public void DisableInput()
    {
        _inputEnabled = false;

        movement.SetMoveInput(Vector2.zero);
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (!_inputEnabled)
            return;

        combat.RequestAttack();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (!_inputEnabled)
            return;

        movement.SetMoveInput(context.ReadValue<Vector2>());
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!_inputEnabled)
            return;

        movement.RequestJump();
    }
}