using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using Voody.UniLeo;

public class EcsStartup : MonoBehaviour {
    private EcsWorld _world;
    private EcsSystems _systems;

    [SerializeField] private TMPro.TextMeshProUGUI _playerEarnText;
    
    void Start() {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _systems.ConvertScene();

        _systems.Inject(_playerEarnText);
            
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
        _systems
            .Add(new StackSetupSystem())
            
            .Add(new PlayerInputSystem())
            .Add(new MovementSystem())
            
            .Add(new DelaySystem())
            .Add(new DelayProgressDisplaySystem())
            .Add(new StackDisplayingSystem())
            
            .Add(new StackGenerationSystem())
            
            .Add(new AgentsStackFillingSystem())
            .Add(new AgentsStackDrainingSystem())
            
            .Add(new RequestedOrderFulfieldSystem())
        
            .Add(new RewardSystem());
    }
}
