using Leopotam.Ecs;
using Components;
using UnityEngine;

sealed class PlayerInputSystem: IEcsRunSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<PlayerControllerComponent, DirectionComponent> _playerDirectionFilter = null;

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
            direction.z = _movementVector.y;
        }
    }

    private void InitializeMovementVector() {
        foreach(var particularEntity in _playerDirectionFilter) {
            ref var controllerComponent = ref _playerDirectionFilter.Get1(particularEntity);

            _movementVector.x = controllerComponent.HorizontalInput;
            _movementVector.y = controllerComponent.VerticalInput;
        }

    }
}