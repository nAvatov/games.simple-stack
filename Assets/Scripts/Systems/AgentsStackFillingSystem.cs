using Leopotam.Ecs;
using Components;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

sealed class AgentsStackFillingSystem : IEcsRunSystem{
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<AgentComponent, StackComponent, DelayComponent> _agentStackFilter = null;
    private readonly EcsFilter<StackInteractorComponent, StackGeneratorComponent, StackComponent, StackVisualizationComponent> _stackInteractionFilter = null;
    public void Run() {
        InitializeStackFilling();
    }

    private void InitializeStackFilling() {
        foreach(var entity in _stackInteractionFilter) {
            ref var stackInteractorComponent = ref _stackInteractionFilter.Get1(entity);
            ref var generatorStackComponent = ref _stackInteractionFilter.Get3(entity);
            ref var generatorStackVisualizeComponent = ref _stackInteractionFilter.Get4(entity);
            foreach(var agentEntity in _agentStackFilter) {
                ref var agentComponent = ref _agentStackFilter.Get1(agentEntity);
                ref var agentStackComponent = ref _agentStackFilter.Get2(agentEntity);
                ref var agentLootDelayComponent = ref _agentStackFilter.Get3(agentEntity);
                if (agentLootDelayComponent.IsTimerExpired) {
                    if (Vector3.Distance(agentComponent.Position, stackInteractorComponent.InteractionPoint) <= stackInteractorComponent.AcceptableInteractionDistance) {
                        generatorStackVisualizeComponent.IsVisualizationRequired = false;
                        TryFillAgentsStack(agentComponent, generatorStackComponent, agentStackComponent, ref generatorStackVisualizeComponent);
                        generatorStackVisualizeComponent.IsVisualizationRequired = true;
                    }

                    agentLootDelayComponent.IsTimerExpired = false;
                    agentLootDelayComponent.TimerState = agentLootDelayComponent.TimerDuration;
                }
            }
        }
    }

    private void TryFillAgentsStack(AgentComponent agentComponent, StackComponent interactorsStackComponent, StackComponent agentStackComponent, ref StackVisualizationComponent generatorStackVisualizeComponent) {
        if (agentStackComponent.Stack == null) {
            agentStackComponent.Stack = new Stack<GameObject>();
        }

        
 
        while(agentStackComponent.Stack.Count < agentComponent.CollectingRestriction && interactorsStackComponent.Stack.Count > 0) {
            if (interactorsStackComponent.Stack.TryPop(out GameObject result)) {
                agentStackComponent.Stack.Push(result);
                CollectItem(agentComponent, result);

                generatorStackVisualizeComponent.NextPlacementPositionIndex--;
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