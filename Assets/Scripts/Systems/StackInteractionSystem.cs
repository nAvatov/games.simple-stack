using Leopotam.Ecs;
using Components;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

sealed class StackInteractionSystem : IEcsInitSystem, IEcsDestroySystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<PlayerTagComponent, StackComponent> _playerStackFilter = null;
    private readonly EcsFilter<StackInteractorComponent, StackComponent> _stackInteractionFilter = null;
    private CancellationTokenSource _stackInteractionCTS;
    public void Init() {
        _stackInteractionCTS = new CancellationTokenSource();
        InitializeStackFilling();
    }

    private void InitializeStackFilling() {
        foreach(var generatorEntity in _stackInteractionFilter) {
            ref var stackInteractorComponent = ref _stackInteractionFilter.Get1(generatorEntity);
            ref var interactorsStackComponent = ref _stackInteractionFilter.Get2(generatorEntity);
            foreach(var playerEntity in _playerStackFilter) {
                ref var playerTagComponent = ref _playerStackFilter.Get1(playerEntity);
                ref var playerStackComponent = ref _playerStackFilter.Get2(playerEntity);
                ProceedStackFill(playerTagComponent, playerStackComponent, stackInteractorComponent, interactorsStackComponent, _stackInteractionCTS.Token);
            }
        }
    }

    private async void ProceedStackFill(PlayerTagComponent playerTagComponent, StackComponent playerStackComponent, StackInteractorComponent stackInteractorComponent, StackComponent interactorsStackComponent, CancellationToken token) {
        while (!token.IsCancellationRequested) {
            if (Vector3.Distance(playerTagComponent.Position, stackInteractorComponent.InteractionPoint) <= stackInteractorComponent.AcceptableInteractionDistance) {
                if (stackInteractorComponent.IsGenerator) {
                    FillPlayersStack(playerTagComponent, interactorsStackComponent, playerStackComponent);
                    await Task.Delay(playerTagComponent.CollectingDelay);
                } else {
                    DrainPlayerStack(playerTagComponent, stackInteractorComponent, interactorsStackComponent, playerStackComponent);
                    await Task.Delay(stackInteractorComponent.DrainDuration);
                }
            }
            await Task.Delay(10);
        }
    }

    private void FillPlayersStack(PlayerTagComponent playerTagComponent, StackComponent interactorsStackComponent, StackComponent playerStackComponent) {
        if (playerStackComponent.Stack == null) {
            playerStackComponent.Stack = new Stack<GameObject>();
        }

        for(int i = 0; i < playerTagComponent.CollectingAmount; i++) {
            if (interactorsStackComponent.Stack.TryPop(out GameObject result)) {
                playerStackComponent.Stack.Push(result);
            }
        }
    }

    private void DrainPlayerStack(PlayerTagComponent playerTagComponent, StackInteractorComponent stackInteractorComponent, StackComponent interactorsStackComponent, StackComponent playerStackComponent) {
        for (int i = 0; i < stackInteractorComponent.RequestedDrainAmount; i++) {
            if (playerStackComponent.Stack.TryPop(out GameObject result)) {
                interactorsStackComponent.Stack.Push(result);
            }
        }
    }

    public void Destroy() {
        _stackInteractionCTS?.Cancel();
    }
}