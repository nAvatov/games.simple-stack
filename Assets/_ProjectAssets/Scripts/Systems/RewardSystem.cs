using System.Collections;
using System.Collections.Generic;
using Components;
using Components.Components;
using Components.Events;
using Leopotam.Ecs;
using UnityEngine;

public class RewardSystem : IEcsRunSystem, IEcsInitSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<RewardEventComponent, StackDrainerComponent> _rewardEventsFilter = null;

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
            ref var drainerComponent = ref _rewardEventsFilter.Get2(rewardEntity);

            SpawnCashBlock();
            
            _rewardEventsFilter.GetEntity(rewardEntity).Del<RewardEventComponent>();
        }
    }

    private void SpawnCashBlock()
    {
        
    }
}
