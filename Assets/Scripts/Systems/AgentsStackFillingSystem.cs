using Leopotam.Ecs;
using Components;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

sealed class AgentsStackFillingSystem : IEcsRunSystem{
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<AgentComponent, StackComponent, DelayComponent> _agentStackFilter = null;
    private readonly EcsFilter<StackInteractorComponent, DelayComponent, StackComponent, StackGeneratorComponent> _stackInteractionFilter = null;
    public void Run() {
        InitializeStackFilling();
    }

    private void InitializeStackFilling() {
        foreach(var entity in _stackInteractionFilter) {
            ref var stackInteractorComponent = ref _stackInteractionFilter.Get1(entity);
            ref var generatorDelayComponent = ref _stackInteractionFilter.Get2(entity);
            ref var generatorStackComponent = ref _stackInteractionFilter.Get3(entity);
            ref var generatorComponent = ref _stackInteractionFilter.Get4(entity);
            foreach(var agentEntity in _agentStackFilter) {
                ref var agentComponent = ref _agentStackFilter.Get1(agentEntity);
                ref var agentStackComponent = ref _agentStackFilter.Get2(agentEntity);
                ref var agentLootDelayComponent = ref _agentStackFilter.Get3(agentEntity);
                if (agentLootDelayComponent.IsTimerExpired) {
                    if (Vector3.Distance(agentComponent.Position, stackInteractorComponent.InteractionPoint) <= stackInteractorComponent.AcceptableInteractionDistance) {
                        // Blocking delay system on particular generator entity. TODO
                        generatorDelayComponent.TimerState = -1f;
                        TryFillAgentsStack(agentComponent, generatorStackComponent, agentStackComponent, ref generatorComponent);
                    }

                    agentLootDelayComponent.IsTimerExpired = false;
                    agentLootDelayComponent.TimerState = agentLootDelayComponent.TimerDuration;
                }
            }
        }
    }

    private void TryFillAgentsStack(AgentComponent agentComponent, StackComponent interactorsStackComponent, StackComponent agentStackComponent, ref StackGeneratorComponent generatorComponent) {
        if (agentStackComponent.ObservableStack == null) {
            agentStackComponent.ObservableStack.CreateStack();
        }

        
 
        while(agentStackComponent.ObservableStack.Count < agentComponent.CollectingRestriction && interactorsStackComponent.ObservableStack.Count > 0) {
            if (interactorsStackComponent.ObservableStack.TryPop(out GameObject result)) {
                agentStackComponent.ObservableStack.Push(result);
                CollectItem(agentComponent, result);

                generatorComponent.NextPlacementPositionIndex--;
            }
        }
    }

    private void CollectItem(AgentComponent agentComponent, GameObject item) {
        item.transform.SetParent(agentComponent.CollectedItemsPlacement);
        item.transform.DOMove(agentComponent.AvaiableStackSpot, 0.5f);
        
        Vector3 newAvaiableSpot = agentComponent.AvaiableStackSpot;
        newAvaiableSpot[1] += 0.55f;
        agentComponent.AvaiableStackSpot = newAvaiableSpot;
    }
}