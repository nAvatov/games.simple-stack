using Leopotam.Ecs;
using Components;
using System.Threading;
using System.Collections.Generic;

sealed class StackDisplayingSystem : IEcsInitSystem, IEcsDestroySystem, IEcsRunSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<StackComponent> _stackFilter = null;
    private readonly EcsFilter<RewardComponent> _rewardFilter = null;
    private CancellationTokenSource _stackDisplayingCTS;

    public void Destroy() {
        _stackDisplayingCTS?.Cancel();
        _stackDisplayingCTS?.Dispose();
    }

    public void Init() {
        InitializeStackDisplaying();
        InitializeRewardDisplaying();
    }

    private void InitializeStackDisplaying()
    {
        foreach (var entity in _stackFilter)
        {
            ref var stackComponent = ref _stackFilter.Get1(entity);

            if (stackComponent.ObservableStack.Stack == null)
            {
                stackComponent.ObservableStack?.CreateStack();
            }
        }
    }

    private void InitializeRewardDisplaying()
    {
        foreach (var entity in _rewardFilter)
        {
            ref var rewardComponent = ref _stackFilter.Get1(entity);

            if (rewardComponent.ObservableStack.Stack == null)
            {
                rewardComponent.ObservableStack?.CreateStack();
            }
        }
    }

    public void Run() {
        DisplayStackAmount();
        DisplayRewardAmount();
    }

    private void DisplayRewardAmount()
    {
        foreach(var entity in _rewardFilter) {
            ref var rewardComponent = ref _rewardFilter.Get1(entity);
            if (rewardComponent.ObservableStack != null && rewardComponent.RewardAmountTMP != null) {
                rewardComponent.RewardAmountTMP.SetText((rewardComponent.IsTitleNeeded ? "Stacked: " : "") + rewardComponent.ObservableStack.Count.ToString() + " " + "$");
            }       
        }
    }

    private void DisplayStackAmount() {
        foreach(var entity in _stackFilter) {
            ref var stackComponent = ref _stackFilter.Get1(entity);
            if (stackComponent.ObservableStack != null && stackComponent.StackAmountTMP != null) {
                stackComponent.StackAmountTMP.SetText((stackComponent.IsTitleNeeded ? "Stacked: " : "") + stackComponent.ObservableStack.Count.ToString());
            }       
        }
    }
}

