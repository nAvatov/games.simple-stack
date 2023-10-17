using Leopotam.Ecs;
using Components;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

sealed class AgentsStackFillingSystem : IEcsInitSystem, IEcsDestroySystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<AgentComponent, StackComponent> _agentStackFilter = null;
    private readonly EcsFilter<StackInteractorComponent, StackGeneratorComponent, StackComponent> _stackInteractionFilter = null;
    private CancellationTokenSource _stackFillingCTS;
    public void Init() {
        _stackFillingCTS = new CancellationTokenSource();
        InitializeStackFilling();
    }

    private void InitializeStackFilling() {
        foreach(var entity in _stackInteractionFilter) {
            ref var stackInteractorComponent = ref _stackInteractionFilter.Get1(entity);
            ref var interactorsStackComponent = ref _stackInteractionFilter.Get3(entity);
            foreach(var agentEntity in _agentStackFilter) {
                ref var agentComponent = ref _agentStackFilter.Get1(agentEntity);
                ref var agentStackComponent = ref _agentStackFilter.Get2(agentEntity);
                ProceedStackInteraction(agentComponent, agentStackComponent, stackInteractorComponent, interactorsStackComponent, _stackFillingCTS.Token);
            }
        }
    }

    private async void ProceedStackInteraction(AgentComponent agentComponent, StackComponent agentStackComponent, StackInteractorComponent stackInteractorComponent, StackComponent interactorsStackComponent, CancellationToken token) {
        while (!token.IsCancellationRequested) {
            if (Vector3.Distance(agentComponent.Position, stackInteractorComponent.InteractionPoint) <= stackInteractorComponent.AcceptableInteractionDistance) {
                FillAgentsStack(agentComponent, interactorsStackComponent, agentStackComponent);
            }
            await Task.Delay(agentComponent.CollectingDelay);
        }
    }

    private void FillAgentsStack(AgentComponent agentComponent, StackComponent interactorsStackComponent, StackComponent agentStackComponent) {
        if (agentStackComponent.Stack == null) {
            agentStackComponent.Stack = new Stack<GameObject>();
        }

        while(agentStackComponent.Stack.Count < agentComponent.CollectingRestriction) {
            if (interactorsStackComponent.Stack.TryPop(out GameObject result)) {
                agentStackComponent.Stack.Push(result);
            }
        }
    }

    public void Destroy() {
        _stackFillingCTS?.Cancel();
        _stackFillingCTS?.Dispose();
    }
}