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
                GenerateItems(stackComponent, ref stackInteractorComponent);
                delayComponent.IsTimerExpired = false;
                delayComponent.TimerState = delayComponent.TimerDuration;
            }
        }
    }

    private void GenerateItems(StackComponent stackComponent, ref StackGeneratorComponent stackGeneratorComponent) {
        for(int i = 0; i < stackGeneratorComponent.GenerationAmount; i++) {

            var stackObjectPrefab = Resources.Load<GameObject>("Burger");
            var obj = GameObject.Instantiate(stackObjectPrefab);
            
            PlaceNewItem(obj, ref stackGeneratorComponent);

            stackComponent.Stack.Push(obj);
        }
    }

    private void PlaceNewItem(GameObject item, ref StackGeneratorComponent stackGeneratorComponent) {
        Debug.Log(stackGeneratorComponent.NextPlacementPositionIndex);
        
        if (stackGeneratorComponent.NextPlacementPositionIndex < 0 || stackGeneratorComponent.NextPlacementPositionIndex >= stackGeneratorComponent.GenerationSpotsHolder.childCount) {
            stackGeneratorComponent.GenerationSpotsHolder.localPosition = new Vector3(0f, stackGeneratorComponent.GenerationSpotsHolder.position.y + 0.1f, 0f);
            stackGeneratorComponent.NextPlacementPositionIndex = 0;
        }

        item.transform.parent = stackGeneratorComponent.GenerationSpotsHolder.transform.GetChild(stackGeneratorComponent.NextPlacementPositionIndex);
        item.transform.localPosition = new Vector3(0,0,0);
        item.transform.parent = stackGeneratorComponent.GenerationCollector;
        stackGeneratorComponent.NextPlacementPositionIndex++;
    }
}   