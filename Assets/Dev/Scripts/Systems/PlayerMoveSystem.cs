using UnityEngine;
using Leopotam.Ecs;

public class PlayerMoveSystem : IEcsRunSystem
{
    private EcsFilter<Player, PlayerInputData> filter;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref var player = ref filter.Get1(i);
            ref var input = ref filter.Get2(i);

            float yVelocity = player.characterController.velocity.y;

            if (player.characterController.isGrounded || player.characterController.collisionFlags == CollisionFlags.Above) yVelocity = -0.1f;

            if (player.characterController.isGrounded)
            {
                player.characterController.slopeLimit = player.originalSlopeLimit;
            }
            else
            {
                player.characterController.slopeLimit = player.jumpSlopeLimit;
            }

            Vector3 move = (player.transform.right * input.MoveHorizontalAxis + player.transform.forward * input.MoveVerticalAxis).normalized;
            move = move * player.speed * Time.deltaTime;

            if (input.IsTryingToRun) move *= player.runMultiplier;

            if (input.IsTryingToJump && player.characterController.isGrounded)
            {
                yVelocity += player.jumpMultiplier;
            }

            yVelocity += player.gravity * Time.deltaTime;

            move.y = yVelocity * Time.deltaTime;

            player.characterController.Move(move);
        }
    }   
}