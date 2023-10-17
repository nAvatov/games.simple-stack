using UnityEngine;
using Leopotam.Ecs;
using Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

sealed class StackGenerationSystem : IEcsInitSystem, IEcsDestroySystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<StackGeneratorComponent, StackComponent> _stackInteractorsFilter = null;
    private CancellationTokenSource _generationCTS;

    public void Destroy() {
        _generationCTS?.Cancel();
        _generationCTS?.Dispose();
    }

    public void Init() {
        _generationCTS = new CancellationTokenSource();
        InitStackGenerating();
    }

    private void InitStackGenerating() {
        foreach(var entity in _stackInteractorsFilter) {
            ref var stackComponent = ref _stackInteractorsFilter.Get2(entity);
            ref var stackInteractorComponent = ref _stackInteractorsFilter.Get1(entity);
            StartGenerating(stackComponent, stackInteractorComponent, _generationCTS.Token);
        }
    }

    private async void StartGenerating(StackComponent stackComponent, StackGeneratorComponent stackGeneratorComponent, CancellationToken token) {
        while(!token.IsCancellationRequested) {
            GenerateItems(stackComponent, stackGeneratorComponent);
            await Task.Delay(stackGeneratorComponent.StackGenerationTime);
        }
    }

    private void GenerateItems(StackComponent stackComponent, StackGeneratorComponent stackGeneratorComponent) {
        for(int i = 0; i < stackGeneratorComponent.GenerationAmount; i++) {
            GameObject newItem = new GameObject();
            newItem.transform.parent = stackGeneratorComponent.GenerationSpot;
            stackComponent.Stack.Push(newItem);
        }
    }
}   