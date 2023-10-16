using UnityEngine;
using Leopotam.Ecs;
using Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

sealed class StackGenerationSystem : IEcsInitSystem, IEcsDestroySystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<StackInteractorComponent, StackComponent> _stackInteractorsFilter = null;
    private readonly EcsFilter<PlayerTagComponent, StackComponent> _playerStackFilter = null;
    private CancellationTokenSource _generationCTS;

    public void Destroy() {
        _generationCTS?.Cancel();
    }

    public void Init() {
        _generationCTS = new CancellationTokenSource();
        InitStackGenerating();
    }

    private void InitStackGenerating() {
        foreach(var entity in _stackInteractorsFilter) {
            ref var stackComponent = ref _stackInteractorsFilter.Get2(entity);
            ref var stackInteractorComponent = ref _stackInteractorsFilter.Get1(entity);
            if (stackInteractorComponent.IsGenerator) {
                stackComponent.Stack = new Stack<GameObject>();
                StartGenerating(stackComponent, stackInteractorComponent, _generationCTS.Token);
            }
        }
    }

    private async void StartGenerating(StackComponent stackComponent, StackInteractorComponent stackGeneratorComponent, CancellationToken token) {
        while(!token.IsCancellationRequested) {
            GameObject newItem = new GameObject();
            newItem.transform.parent = stackGeneratorComponent.GenerationSpot;
            stackComponent.Stack.Push(newItem);
            await Task.Delay(stackGeneratorComponent.StackGenerationTime);
        }
    }
}   