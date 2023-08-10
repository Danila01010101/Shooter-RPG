using UnityEngine;
using Leopotam.Ecs;

public class PlayerLookSystem : IEcsRunSystem
{
	private EcsFilter<Player, PlayerInputData> filter;

    public void Run()
    {
        foreach (var i in filter)
        {
            ref var player = ref filter.Get1(i);
            ref var input = ref filter.Get2(i);

            float mouseX = input.MouseXAxis * player.mouseSensivity * Time.deltaTime;
            float mouseY = input.MouseYAxis * player.mouseSensivity * Time.deltaTime;

            player.xRotation -= mouseY;
            player.xRotation = Mathf.Clamp(player.xRotation, player.minLookAngle, player.maxLookAngle);

            player.camera.transform.localRotation = Quaternion.Euler(player.xRotation, 0f, 0f);
            player.transform.Rotate(Vector3.up * mouseX);
        }
    }
}