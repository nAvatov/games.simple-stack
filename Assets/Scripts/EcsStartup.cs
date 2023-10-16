using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;

public class EcsStartup : MonoBehaviour {
    private EcsWorld _world;
    private EcsSystems _systems;
    void Start() {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _systems.ConvertScene();
        AddSystems();
        _systems.Init();
    }
    
    void Update () {
        _systems.Run ();
    }

    void OnDestroy () {
        _systems.Destroy ();
        _world.Destroy ();
    }

    private void AddSystems() {
        _systems
            .Add(new PlayerInputSystem())
            .Add(new MovementSystem())
            .Add(new StackDisplayingSystem())
            .Add(new StackGenerationSystem())
            .Add(new StackInteractionSystem());
    }
}
