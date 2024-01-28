using UnityEngine;
using Leopotam.Ecs;
using Components;

sealed class StackVisualizationSystem : IEcsRunSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<StackComponent, StackVisualizationComponent> _stackVisualizationFilter = null;

    public void Run() {
        foreach(var entity in  _stackVisualizationFilter) {
            ref var stackComponent = ref _stackVisualizationFilter.Get1(entity);
            ref var stackVisualizationComponent = ref _stackVisualizationFilter.Get2(entity);
            
            if (stackComponent.Stack.Count < stackComponent.StackAmount) {
                VisualizeStack(ref stackComponent, ref stackVisualizationComponent);
            }
        }
    }

    private void VisualizeStack(ref StackComponent stackComponent, ref StackVisualizationComponent stackVisualizationComponent) {
        if (stackComponent.Stack.Count >= stackVisualizationComponent.GenerationChunkAmount) {
            // At this point we're not spawning new objects, but increasing the StackAmount
            return; 
        }

        var stackObjectPrefab = Resources.Load<GameObject>(stackVisualizationComponent.ResourceName); // TODO
        var obj = GameObject.Instantiate(stackObjectPrefab);
        stackComponent.Stack.Push(obj);

        PlaceNewItem(obj, ref stackVisualizationComponent);
    }

    private void PlaceNewItem(GameObject item, ref StackVisualizationComponent stackVisualizationComponent) {
        if (stackVisualizationComponent.NextPlacementPositionIndex < 0 || stackVisualizationComponent.NextPlacementPositionIndex >= stackVisualizationComponent.GenerationSpotsHolder.childCount) {
            Vector3 newSpotsPosition = stackVisualizationComponent.GenerationSpotsHolder.position;
            newSpotsPosition[1] = stackVisualizationComponent.GenerationSpotsHolder.position.y + 0.6f;
            stackVisualizationComponent.GenerationSpotsHolder.position = newSpotsPosition;
            stackVisualizationComponent.NextPlacementPositionIndex = 0;
        }

        Transform spotTransform = stackVisualizationComponent.GenerationSpotsHolder.transform.GetChild(stackVisualizationComponent.NextPlacementPositionIndex);
        item.transform.SetParent(spotTransform, true);
        item.transform.localPosition = new Vector3(0, 0, 0);
        
        item.transform.SetParent(stackVisualizationComponent.GenerationCollector, true);
        stackVisualizationComponent.NextPlacementPositionIndex++;
    }
}