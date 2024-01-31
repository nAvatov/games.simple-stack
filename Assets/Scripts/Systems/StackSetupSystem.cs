using Leopotam.Ecs;
using Components;
using UnityEngine;

sealed class StackSetupSystem : IEcsInitSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<StackComponent> _stackFilter = null;

    public void Init() {
        foreach(var entity in _stackFilter) {
            ref var stackComponent = ref _stackFilter.Get1(entity);
            stackComponent.ObservableStack = new ObservableStack<GameObject>();     
            stackComponent.ObservableStack.CreateStack();
        }
    }
}
