using Leopotam.Ecs;
using Components;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

sealed class AgentsStackDrainingSystem : IEcsInitSystem, IEcsDestroySystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<AgentComponent, StackComponent> _agentStackFilter = null;
    private readonly EcsFilter<StackInteractorComponent, StackDrainerComponent, StackComponent> _stackInteractionFilter = null;
    private CancellationTokenSource _stackInteractionCTS;
    private System.Random _drainRandomizer;
    public void Init() {
        _drainRandomizer = new System.Random();
        _stackInteractionCTS = new CancellationTokenSource();
        InitializeStackInteraction();
    }

    private void InitializeStackInteraction() {
        foreach(var entity in _stackInteractionFilter) {
            ref var stackInteractorComponent = ref _stackInteractionFilter.Get1(entity);
            ref var stackDrainerComponent = ref _stackInteractionFilter.Get2(entity);
            stackDrainerComponent.RequestedDrainAmount = _drainRandomizer.Next(1, stackDrainerComponent.MaxRequestAmount);
            ref var interactorsStackComponent = ref _stackInteractionFilter.Get3(entity);
            foreach(var agentEntity in _agentStackFilter) {
                ref var agentComponent = ref _agentStackFilter.Get1(agentEntity);
                ref var agentStackComponent = ref _agentStackFilter.Get2(agentEntity);
                ProceedStackInteraction(agentComponent, agentStackComponent, stackInteractorComponent, interactorsStackComponent, stackDrainerComponent, _stackInteractionCTS.Token);
            }
        }
    }

    private async void ProceedStackInteraction(
        AgentComponent agentComponent, 
        StackComponent agentStackComponent, 
        StackInteractorComponent stackInteractorComponent, 
        StackComponent interactorsStackComponent, 
        StackDrainerComponent stackDrainerComponent,
        CancellationToken token) {
            while (!token.IsCancellationRequested) {
                if (Vector3.Distance(agentComponent.Position, stackInteractorComponent.InteractionPoint) <= stackInteractorComponent.AcceptableInteractionDistance) {
                    DrainAgentsStack(agentComponent, stackInteractorComponent, interactorsStackComponent, agentStackComponent, ref stackDrainerComponent);
                }
                await Task.Delay(stackDrainerComponent.DrainDuration);
            }
    }

    private void DrainAgentsStack(AgentComponent agentComponent, StackInteractorComponent stackInteractorComponent, StackComponent interactorsStackComponent, StackComponent agentStackComponent, ref StackDrainerComponent stackDrainerComponent) {
        if (stackDrainerComponent.RequestedDrainAmount == interactorsStackComponent.Stack.Count) {
            stackDrainerComponent.RequestedDrainAmount = _drainRandomizer.Next(1, stackDrainerComponent.MaxRequestAmount);
            interactorsStackComponent.Stack.Clear();
            return;
        }

        if (agentStackComponent.Stack.Count == 0) {
            return;
        }
        
        if (agentStackComponent.Stack.TryPop(out GameObject result)) {
            interactorsStackComponent.Stack.Push(result);
        }
    }

    public void Destroy() {
        _stackInteractionCTS?.Cancel();
        _stackInteractionCTS?.Dispose();
    }
}