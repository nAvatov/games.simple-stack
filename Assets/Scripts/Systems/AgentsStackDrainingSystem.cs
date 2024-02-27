using Leopotam.Ecs;
using Components;
using UnityEngine;
using DG.Tweening;


sealed class AgentsStackDrainingSystem : IEcsRunSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<AgentComponent, StackComponent> _agentStackFilter = null;
    private readonly EcsFilter<StackInteractorComponent, StackDrainerComponent, StackComponent> _stackInteractionFilter = null;
    public void Run() {
        InitializeStackInteraction();
    }

    private void InitializeStackInteraction() {
        foreach(var entity in _stackInteractionFilter) {
            ref var stackInteractorComponent = ref _stackInteractionFilter.Get1(entity);
            ref var stackDrainerComponent = ref _stackInteractionFilter.Get2(entity);
            ref var interactorsStackComponent = ref _stackInteractionFilter.Get3(entity);
            foreach(var agentEntity in _agentStackFilter) {
                ref var agentComponent = ref _agentStackFilter.Get1(agentEntity);
                ref var agentStackComponent = ref _agentStackFilter.Get2(agentEntity);
                if (Vector3.Distance(agentComponent.Position, stackInteractorComponent.InteractionPoint) <= stackInteractorComponent.AcceptableInteractionDistance) {
                    DrainAgentsStack(agentComponent, stackInteractorComponent, interactorsStackComponent, agentStackComponent, ref stackDrainerComponent);
                }
            }
        }
    }

    private void DrainAgentsStack(AgentComponent agentComponent, StackInteractorComponent stackInteractorComponent, StackComponent interactorsStackComponent, StackComponent agentStackComponent, ref StackDrainerComponent stackDrainerComponent) {
        if (agentStackComponent.ObservableStack.Count == 0) {
            return;
        }
        
        if (agentStackComponent.ObservableStack.TryPop(out GameObject result)) {
            result.transform.SetParent(stackDrainerComponent.CollectorSpot, true);
            result.transform.DOMove(stackDrainerComponent.AvaiableItemSpot.position, 0.5f)
                .OnComplete(() => { 
                    interactorsStackComponent.ObservableStack.Push(result);
                });
            
            HeightDeltaHandler.HandleSpotPosition(stackDrainerComponent.AvaiableItemSpot, stackDrainerComponent.HeightDelta, true);
            HeightDeltaHandler.HandleSpotPosition(agentComponent.AvaiableStackSpot, agentComponent.SpotHeightDelta, false);
        }
    }
}