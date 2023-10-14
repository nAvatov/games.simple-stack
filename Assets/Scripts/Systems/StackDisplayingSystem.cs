using Leopotam.Ecs;
using Components;

sealed class StackDisplayingSystem : IEcsRunSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<StackComponent> _stackFilter = null;

    public void Run() {
        DisplayStackAmount();
    }

    private void DisplayStackAmount() {
        foreach(var entity in _stackFilter) {
            ref var stackComponent = ref _stackFilter.Get1(entity);
            if (stackComponent.Stack != null) {
                stackComponent.StackAmount = stackComponent.Stack.Count.ToString();
            }
        }
    }
}

