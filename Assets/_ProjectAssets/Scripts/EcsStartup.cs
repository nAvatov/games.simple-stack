using _ProjectAssets.Scripts.Systems;
using _ProjectAssets.Scripts.Systems.Interaction_Systems;
using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;

public class EcsStartup : MonoBehaviour {
    private EcsWorld _world;
    private EcsSystems _systems;

    [SerializeField] private TMPro.TextMeshProUGUI _playerEarnText;
    
    void Start() {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _systems.ConvertScene();

        //_systems.Inject(_playerEarnText);
            
        AddSystems();
        _systems.Init();
    }
    
    void Update () {
        _systems.Run();
    }

    void OnDestroy () {
        _systems.Destroy ();
        _world.Destroy ();
    }

    private void AddSystems() {
        AddCoreSystems();
        AddDisplayingSystems();
        AddGenerationSystems();
        AddAgentStackInteractionSystems();
        AddInteractionResultingSystems();
    }

    private void AddCoreSystems()
    {
        _systems
            .Add(new StackSetupSystem())
            .Add(new PlayerInputSystem())
            .Add(new MovementSystem());
    }

    private void AddDisplayingSystems()
    {
        _systems
            .Add(new DelaySystem())
            .Add(new DelayProgressDisplaySystem())
            .Add(new StackDisplayingSystem());
    }

    private void AddGenerationSystems()
    {
        _systems
            .Add(new StackGenerationSystem());
    }

    private void AddAgentStackInteractionSystems()
    {
        _systems
            .Add(new AgentsStackFillingSystem())
            .Add(new AgentsStackDrainingSystem());
    }

    private void AddInteractionResultingSystems()
    {
        _systems
            .Add(new RequestedOrderFulfieldSystem())
            .Add(new RewardSystem());
    }
}
