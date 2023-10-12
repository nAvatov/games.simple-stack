using UnityEngine;
using Leopotam.Ecs;
using Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

sealed class StackSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<StackComponent> _stackFilter = null;
    private readonly EcsFilter<StackGeneratorComponent, StackComponent> _stackGeneratorFilter = null;
    private readonly EcsFilter<PlayerTagComponent, StackComponent> _playerStackFilter = null;
    private CancellationTokenSource generationCTS;

    public void Destroy() {
        generationCTS.Cancel();
    }

    public void Init() {
        generationCTS = new CancellationTokenSource();
        InitStackGenerating();
    }

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

    private void InitStackGenerating() {
        foreach(var entity in _stackGeneratorFilter) {
            ref var stackComponent = ref _stackGeneratorFilter.Get2(entity);
            ref var stackGeneratorComponent = ref _stackGeneratorFilter.Get1(entity);
            stackComponent.Stack = new Stack<GameObject>();
            StartGenerating(stackComponent, stackGeneratorComponent, generationCTS.Token);
        }
    }

    private async void StartGenerating(StackComponent stackComponent, StackGeneratorComponent stackGeneratorComponent, CancellationToken token) {
        while(!token.IsCancellationRequested) {
            GameObject newItem = new GameObject();
            newItem.transform.parent = stackGeneratorComponent.GenerationSpot;
            stackComponent.Stack.Push(newItem);
            await Task.Delay(stackGeneratorComponent.StackGenerationTime);
        }
    }
}