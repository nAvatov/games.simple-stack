using Leopotam.Ecs;
using Components;
using System.Threading;
using System.Collections.Generic;

sealed class StackDisplayingSystem : IEcsInitSystem, IEcsDestroySystem, IEcsRunSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<StackComponent> _stackFilter = null;
    private CancellationTokenSource _stackDisplayingCTS;

    public void Destroy() {
        _stackDisplayingCTS?.Cancel();
        _stackDisplayingCTS?.Dispose();
    }

    public void Init() {
        foreach(var entity in _stackFilter) {
            ref var stackComponent = ref _stackFilter.Get1(entity);
            if (stackComponent.ObservableStack == null) {
                stackComponent.ObservableStack.CreateStack();
            }       
        }
    }

    public void Run() {
        DisplayStackAmount();
    }

    private void DisplayStackAmount() {
        foreach(var entity in _stackFilter) {
            ref var stackComponent = ref _stackFilter.Get1(entity);
            if (stackComponent.ObservableStack != null) {
                stackComponent.StackAmountTMP.SetText((stackComponent.IsTitleNeeded ? "Stacked: " : "") + stackComponent.ObservableStack.Count.ToString());
            }       
        }
    }
}

