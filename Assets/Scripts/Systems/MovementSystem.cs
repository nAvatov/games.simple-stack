using UnityEngine;
using Leopotam.Ecs;
using Components;

sealed class MovementSystem : IEcsRunSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<PhysicalModelComponent, MovementComponent, DirectionComponent> _movementFilter = null;
    public void Run() {
        ProceedMovement();
    }

    private void ProceedMovement() {
        foreach(var entity in _movementFilter) {
            ref var physicalModelComponent = ref _movementFilter.Get1(entity);
            ref var movementComponent = ref _movementFilter.Get2(entity);
            ref var directionComponent = ref _movementFilter.Get3(entity);

            ref var direction = ref directionComponent.Direction;
            ref var transform = ref physicalModelComponent.Transform;
            
            ref var characterController = ref movementComponent.CharacterController;
            ref var movementSpeed = ref movementComponent.MovementSpeed;

            var moveDirection = (transform.right * direction.x) + (transform.forward * direction.z);
            characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
        }
    }
}