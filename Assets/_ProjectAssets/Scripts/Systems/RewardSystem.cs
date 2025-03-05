using System.Collections;
using System.Collections.Generic;
using Components;
using Components.Components;
using Components.Events;
using Leopotam.Ecs;
using UnityEngine;

public class RewardSystem : IEcsRunSystem, IEcsInitSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<RewardEventComponent, StackGeneratorComponent> _rewardEventsFilter = null;

    public void Init()
    {
        
    }
    
    public void Run()
    {
        ApplyFoundedRewards();
    }

    private void ApplyFoundedRewards()
    {
        foreach (var rewardEntity in _rewardEventsFilter)
        {
            ref var rewardEventComponent = ref _rewardEventsFilter.Get1(rewardEntity);
            

            SpawnCashBlock(rewardEntity);
            
            _rewardEventsFilter.GetEntity(rewardEntity).Del<RewardEventComponent>();
        }
    }

    private void SpawnCashBlock(int entityIndex)
    {
        // Debug.Log("Spawn cash block");
        // ref var generatorComponent = ref _rewardEventsFilter.Get2(entityIndex);
        //
        // if (stackComponent.ObservableStack.Count >= generatorComponent.AvaiableItemSpots.Count) {
        //     // At this point we're not spawning new objects
        //     return; 
        // }
        //
        // var resourceRequest = Resources.LoadAsync(stackGeneratorComponent.ResourceName); // TODO
        // var obj = GameObject.Instantiate(resourceRequest.asset as GameObject);
        // stackComponent.ObservableStack.Push(obj);
        //
        //
        //
        // Transform spotTransform = generatorComponent.AvaiableItemSpots[generatorComponent.NextPlacementPositionIndex];
        // item.transform.SetParent(spotTransform, true);
        // item.transform.localPosition = new Vector3(0, 0, 0);
        //
        // item.transform.SetParent(stackGeneratorComponent.GenerationCollector, true);
        // stackGeneratorComponent.NextPlacementPositionIndex++;
    }
}
