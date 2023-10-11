using Leopotam.Ecs;
using Components;
using UnityEngine;

sealed class PlayerInputSystem: IEcsRunSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<PlayerTagComponent, DirectionComponent> _playerDirectionFilter = null;

    private Vector2 _movementVector;
    public void Run() {
        ProceedInput();
    }

    private void ProceedInput() {
        InitializeMovementVector();

        foreach(var particularEntity in _playerDirectionFilter) {
            ref var dirComponent = ref _playerDirectionFilter.Get2(particularEntity);
            ref var direction = ref dirComponent.Direction;
            direction.x = _movementVector.x;
            direction.y = _movementVector.y;
        }
    }

    private void InitializeMovementVector() {
        _movementVector.x = Input.GetAxis("Horizontal");
        _movementVector.y = Input.GetAxis("Vertical");
    }
}