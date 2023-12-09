using Leopotam.Ecs;
using Components;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

sealed class AgentsStackFillingSystem : IEcsRunSystem{
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<AgentComponent, StackComponent, DelayComponent> _agentStackFilter = null;
    private readonly EcsFilter<StackInteractorComponent, StackGeneratorComponent, StackComponent> _stackInteractionFilter = null;
    public void Run() {
        InitializeStackFilling();
    }

    private void InitializeStackFilling() {
        foreach(var entity in _stackInteractionFilter) {
            ref var stackInteractorComponent = ref _stackInteractionFilter.Get1(entity);
            ref var interactorsStackComponent = ref _stackInteractionFilter.Get3(entity);
            foreach(var agentEntity in _agentStackFilter) {
                ref var agentComponent = ref _agentStackFilter.Get1(agentEntity);
                ref var agentStackComponent = ref _agentStackFilter.Get2(agentEntity);
                ref var agentLootDelayComponent = ref _agentStackFilter.Get3(agentEntity);
                if (agentLootDelayComponent.IsTimerExpired) {
                    if (Vector3.Distance(agentComponent.Position, stackInteractorComponent.InteractionPoint) <= stackInteractorComponent.AcceptableInteractionDistance) {
                        TryFillAgentsStack(agentComponent, interactorsStackComponent, agentStackComponent);
                    }

                    agentLootDelayComponent.IsTimerExpired = false;
                    agentLootDelayComponent.TimerState = agentLootDelayComponent.TimerDuration;
                }
            }
        }
    }

    private void TryFillAgentsStack(AgentComponent agentComponent, StackComponent interactorsStackComponent, StackComponent agentStackComponent) {
        if (agentStackComponent.Stack == null) {
            agentStackComponent.Stack = new Stack<GameObject>();
        }

        // while loop for game-improvement features        
        while(agentStackComponent.Stack.Count < agentComponent.CollectingRestriction || interactorsStackComponent.Stack.Count > 0) {
            if (interactorsStackComponent.Stack.TryPop(out GameObject result)) {
                agentStackComponent.Stack.Push(result);
            }
        }
    }
}