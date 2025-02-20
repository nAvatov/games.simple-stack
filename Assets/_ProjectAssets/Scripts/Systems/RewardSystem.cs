using System.Collections;
using System.Collections.Generic;
using Components;
using Components.Components;
using Components.Events;
using Leopotam.Ecs;
using UnityEngine;

public class RewardSystem : IEcsRunSystem, IEcsInitSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<RewardEventComponent> _rewardEventsFilter = null;
    private readonly EcsFilter<PlayerComponent> _playerFilter = null;
    private TMPro.TextMeshProUGUI _playerEarnText;

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

            foreach (var playerEntity in _playerFilter)
            {
                ref var playerComponent = ref _playerFilter.Get1(playerEntity);
                
                playerComponent.EarnedMoneyAmount += rewardEventComponent.RewardAmount;
                _playerEarnText.SetText(playerComponent.EarnedMoneyAmount.ToString());
            }
            
            _rewardEventsFilter.GetEntity(rewardEntity).Del<RewardEventComponent>();
        }
    }
}
