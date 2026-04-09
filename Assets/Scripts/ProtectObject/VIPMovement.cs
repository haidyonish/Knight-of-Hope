using UnityEngine;

public class VIPMovement : EntityMovement
{
    [SerializeField] private Transform player;
    protected override void HandleMovement()
    {
        if ((IsFacingRight && transform.position.x > player.position.x) || (!IsFacingRight && transform.position.x < player.position.x))
            Flip();
    }

    protected override void GroundCheck() { /* Этому дружбану такая проверка не нужна ^-^ */ }
}
