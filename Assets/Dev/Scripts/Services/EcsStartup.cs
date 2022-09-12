using UnityEngine;
using Leopotam.Ecs;

public class EcsStartup : MonoBehaviour
{
    [SerializeField] private PlayerData configuration;
    [SerializeField] private SceneData sceneData;

	EcsWorld _ecsWorld;
	EcsSystems _updateSystems;
	EcsSystems _fixedUpdateSystems;
	EcsSystems _lateUpdateSystems;

    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _updateSystems = new EcsSystems(_ecsWorld);
        _fixedUpdateSystems = new EcsSystems(_ecsWorld);
        _lateUpdateSystems = new EcsSystems(_ecsWorld);
        RuntimeData runtimeData = new RuntimeData();

        _updateSystems
           .Add(new PlayerInitSystem())
           .Add(new PlayerInputSystem())
           .Inject(configuration)
           .Inject(sceneData)
           .Inject(runtimeData);

        _fixedUpdateSystems
            .Add(new PlayerMoveSystem());

        _lateUpdateSystems
           .Add(new PlayerLookSystem());

        _updateSystems.Init();
        _fixedUpdateSystems.Init();
        _lateUpdateSystems.Init();
    }

    private void Update()
    {
        _updateSystems?.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystems?.Run();
    }

    private void LateUpdate()
    {
        _lateUpdateSystems?.Run();
    }

    private void OnDestroy()
    {
        _updateSystems?.Destroy();
        _updateSystems = null;
        _fixedUpdateSystems?.Destroy();
        _fixedUpdateSystems = null;
        _ecsWorld?.Destroy();
        _ecsWorld = null;
    }
}