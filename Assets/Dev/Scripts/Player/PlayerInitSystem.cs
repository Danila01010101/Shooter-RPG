using Leopotam.Ecs;
using UnityEngine;

public class PlayerInitSystem : MonoBehaviour, IEcsInitSystem
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private SceneData _sceneData;

	private EcsWorld _world;

	public void Init()
    {
        EcsEntity playerEntity = _world.NewEntity();

        ref var player = ref playerEntity.Get<Player>();
        ref var inputData = ref playerEntity.Get<PlayerInputData>();

        GameObject playerGameObject = Instantiate(playerData.PlayerPrefab, _sceneData.PlayerSpawnPoint.position, Quaternion.identity);
        player.characterController = playerGameObject.GetComponent<CharacterController>();
        player.camera = Camera.main;
        player.transform = playerGameObject.transform;
        player.xRotation = 0;
        player.speed = playerData.speed;
        player.jumpMultiplier = playerData.jumpMultiplier;
        player.jumpSlopeLimit = playerData.jumpSlopeLimit;
        player.gravity = playerData.gravity;
        player.originalSlopeLimit = playerData.originalSlopeLimit;
        player.runMultiplier = playerData.runMultiplier;
        player.mouseSensivity = playerData.mouseSensitivity;
        player.minLookAngle = playerData.minLookAngle;
        player.maxLookAngle = playerData.maxLookAngle;
    }
}