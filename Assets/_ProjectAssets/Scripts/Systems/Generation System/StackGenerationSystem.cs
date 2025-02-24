using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using Components;

sealed class StackGenerationSystem : IEcsRunSystem, IEcsInitSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<StackGeneratorComponent, StackComponent, DelayComponent> _stackGeneratorsFilter = null;

    public void Init()
    {
        InitializeSpawnPointsListForGenerators();
    }
    
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
        if (stackComponent.ObservableStack.Count >= stackGeneratorComponent.AvaiableItemSpots.Count) {
            // At this point we're not spawning new objects
            return; 
        }

        var resourceRequest = Resources.LoadAsync(stackGeneratorComponent.ResourceName); // TODO
        var obj = GameObject.Instantiate(resourceRequest.asset as GameObject);
        stackComponent.ObservableStack.Push(obj);

        PlaceNewItem(obj, ref stackGeneratorComponent);
    }

    private void PlaceNewItem(GameObject item, ref StackGeneratorComponent stackGeneratorComponent) {
        Transform spotTransform = stackGeneratorComponent.AvaiableItemSpots[stackGeneratorComponent.NextPlacementPositionIndex];
        item.transform.SetParent(spotTransform, true);
        item.transform.localPosition = new Vector3(0, 0, 0);
        
        item.transform.SetParent(stackGeneratorComponent.GenerationCollector, true);
        stackGeneratorComponent.NextPlacementPositionIndex++;
    }

    private void InitializeSpawnPointsListForGenerators()
    {
        foreach (var entity in _stackGeneratorsFilter) 
        {
            ref var generatorComponent = ref _stackGeneratorsFilter.Get1(entity);

            if (generatorComponent.GenerationSpotsHolder.childCount > 0)
            {
                generatorComponent.AvaiableItemSpots = new List<Transform>();
                
                for (int i = 0; i < generatorComponent.GenerationSpotsHolder.childCount; i++)
                {
                    generatorComponent.AvaiableItemSpots.Add(generatorComponent.GenerationSpotsHolder.GetChild(i));
                }
            }
        }
    }
}   