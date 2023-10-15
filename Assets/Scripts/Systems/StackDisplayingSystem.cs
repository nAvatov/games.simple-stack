using Leopotam.Ecs;
using Components;
using System.Threading;
using System.Threading.Tasks;

sealed class StackDisplayingSystem : IEcsInitSystem, IEcsDestroySystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<StackComponent> _stackFilter = null;
    private CancellationTokenSource stackDisplayingCTS;

    public void Destroy() {
        stackDisplayingCTS?.Cancel();
    }

    public void Init() {
        stackDisplayingCTS = new CancellationTokenSource();
        InitializeStackDisplaying();
    }

    private void InitializeStackDisplaying() {
        foreach(var entity in _stackFilter) {
            ref var stackComponent = ref _stackFilter.Get1(entity);
            ObserveStackChanges(stackComponent, stackDisplayingCTS.Token);
        }
    }

    private async void ObserveStackChanges(StackComponent stackComponent, CancellationToken token) {
        while (!stackDisplayingCTS.Token.IsCancellationRequested) {
            stackComponent.StackAmount = stackComponent.Stack?.Count.ToString();
            await Task.Delay(10);
        }
    }
}

