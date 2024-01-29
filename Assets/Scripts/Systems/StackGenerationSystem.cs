using UnityEngine;
using Leopotam.Ecs;
using Components;

sealed class StackGenerationSystem : IEcsRunSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<StackGeneratorComponent, StackComponent, DelayComponent> _stackGeneratorsFilter = null;

    public void Run() {
        foreach(var entity in _stackGeneratorsFilter) {
            ref var stackComponent = ref _stackGeneratorsFilter.Get2(entity);
            ref var stackInteractorComponent = ref _stackGeneratorsFilter.Get1(entity);
            ref var delayComponent = ref _stackGeneratorsFilter.Get3(entity);

            if (delayComponent.IsTimerExpired) {
                GenerateItems(ref stackComponent, stackInteractorComponent);
                
                delayComponent.IsTimerExpired = false;
                delayComponent.TimerState = delayComponent.TimerDuration;
            }
        }
    }


    // In seeking of optimization increasing some uint value as real stack amount and using Stack<GameObject> as
    // visualizing tool is better option.
    private void GenerateItems(ref StackComponent stackComponent, StackGeneratorComponent stackGeneratorComponent) {
        stackComponent.StackAmount += stackGeneratorComponent.GenerationAmount;
    }
}   