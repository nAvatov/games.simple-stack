using Leopotam.Ecs;
using Components;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

sealed class StackFillingSystem : IEcsInitSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<PlayerTagComponent, StackComponent> _playerStackFilter = null;
    private readonly EcsFilter<StackGeneratorComponent, StackComponent> _stackGenerationFilter = null;
    private CancellationTokenSource _playerStackFillCTS;
    public void Init() {
        _playerStackFillCTS = new CancellationTokenSource();
        InitializeStackFilling();
    }

    private void InitializeStackFilling() {
        foreach(var generatorEntity in _stackGenerationFilter) {
            ref var stackGeneratorComponent = ref _stackGenerationFilter.Get1(generatorEntity);
            ref var generatorsStackComponent = ref _stackGenerationFilter.Get2(generatorEntity);
            foreach(var playerEntity in _playerStackFilter) {
                ref var playerTagComponent = ref _playerStackFilter.Get1(playerEntity);
                ref var playerStackComponent = ref _playerStackFilter.Get2(playerEntity);
                ProceedStackFill(playerTagComponent, playerStackComponent, stackGeneratorComponent, generatorsStackComponent, _playerStackFillCTS.Token);
            }
        }
    }

    private async void ProceedStackFill(PlayerTagComponent playerTagComponent, StackComponent playerStackComponent, StackGeneratorComponent stackGeneratorComponent, StackComponent generatorsStackComponent, CancellationToken token) {
        while (!token.IsCancellationRequested) {
            if (Vector3.Distance(playerTagComponent.Position, stackGeneratorComponent.CollectPoint) <= stackGeneratorComponent.CollectDistance) {
                FillPlayersStack(playerTagComponent, generatorsStackComponent, playerStackComponent);
            }
            await Task.Delay(playerTagComponent.CollectingDelay);
        }
    }

    private void FillPlayersStack(PlayerTagComponent playerTagComponent, StackComponent generatorsStackComponent, StackComponent playerStackComponent) {
        if (playerStackComponent.Stack == null) {
            playerStackComponent.Stack = new Stack<GameObject>();
        }

        for(int i = 0; i < playerTagComponent.CollectingAmount; i++) {
            if (generatorsStackComponent.Stack.TryPop(out GameObject result)) {
                playerStackComponent.Stack.Push(result);
            }
        }
    }

}