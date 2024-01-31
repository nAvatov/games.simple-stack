using UnityEngine;
using Leopotam.Ecs;
using Components;

sealed class StackGenerationSystem : IEcsRunSystem{
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<StackGeneratorComponent, StackComponent, DelayComponent> _stackGeneratorsFilter = null;

    public void Run() {
        foreach(var entity in _stackGeneratorsFilter) {
            ref var stackComponent = ref _stackGeneratorsFilter.Get2(entity);
            ref var stackInteractorComponent = ref _stackGeneratorsFilter.Get1(entity);
            ref var delayComponent = ref _stackGeneratorsFilter.Get3(entity);

            if (delayComponent.IsTimerExpired) {
                GenerateItems(ref stackComponent, ref stackInteractorComponent);
                
                delayComponent.IsTimerExpired = false;
                delayComponent.TimerState = delayComponent.TimerDuration;
            }
        }
    }


    // In seeking of optimization increasing some uint value as real stack amount and using Stack<GameObject> as
    // visualizing tool is better option.
    private void GenerateItems(ref StackComponent stackComponent, ref StackGeneratorComponent stackGeneratorComponent) {
        if (stackComponent.ObservableStack.Count >= stackGeneratorComponent.GenerationAmountRestriction) {
            // At this point we're not spawning new objects
            return; 
        }

        var stackObjectPrefab = Resources.Load<GameObject>(stackGeneratorComponent.ResourceName); // TODO
        var obj = GameObject.Instantiate(stackObjectPrefab);
        stackComponent.ObservableStack.Push(obj);

        PlaceNewItem(obj, ref stackGeneratorComponent);
    }

    private void PlaceNewItem(GameObject item, ref StackGeneratorComponent stackGeneratorComponent) {
        if (stackGeneratorComponent.NextPlacementPositionIndex < 0 || stackGeneratorComponent.NextPlacementPositionIndex >= stackGeneratorComponent.GenerationSpotsHolder.childCount) {
            Vector3 newSpotsPosition = stackGeneratorComponent.GenerationSpotsHolder.position;
            newSpotsPosition[1] = stackGeneratorComponent.GenerationSpotsHolder.position.y + 0.6f;
            stackGeneratorComponent.GenerationSpotsHolder.position = newSpotsPosition;
            stackGeneratorComponent.NextPlacementPositionIndex = 0;
        }

        Transform spotTransform = stackGeneratorComponent.GenerationSpotsHolder.transform.GetChild(stackGeneratorComponent.NextPlacementPositionIndex);
        item.transform.SetParent(spotTransform, true);
        item.transform.localPosition = new Vector3(0, 0, 0);
        
        item.transform.SetParent(stackGeneratorComponent.GenerationCollector, true);
        stackGeneratorComponent.NextPlacementPositionIndex++;
    }
}   