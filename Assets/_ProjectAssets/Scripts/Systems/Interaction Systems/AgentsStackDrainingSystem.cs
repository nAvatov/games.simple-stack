using _ProjectAssets.Scripts.Components;
using Components;
using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace _ProjectAssets.Scripts.Systems.Interaction_Systems
{
    /// <summary>
    /// System responsible for drain stack items FROM AGENT TO DRAINER BY BROCKER
    /// </summary>
    sealed class AgentsStackDrainingSystem : IEcsRunSystem {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<AgentComponent, StackComponent> _agentStackFilter = null;
        private readonly EcsFilter<StackBrockerComponent, StackDrainerComponent, StackComponent> _stackInteractionFilter = null;
        public void Run() {
            InitializeStackInteraction();
        }

        private void InitializeStackInteraction() {
            foreach(var entity in _stackInteractionFilter) {
                ref var stackBrockerComponent = ref _stackInteractionFilter.Get1(entity);
                ref var stackDrainerComponent = ref _stackInteractionFilter.Get2(entity);
                ref var interactorsStackComponent = ref _stackInteractionFilter.Get3(entity);
                foreach(var agentEntity in _agentStackFilter) {
                    ref var agentComponent = ref _agentStackFilter.Get1(agentEntity);
                    ref var agentStackComponent = ref _agentStackFilter.Get2(agentEntity);
                    if (Vector3.Distance(agentComponent.Position, stackBrockerComponent.InteractionPoint) <= stackBrockerComponent.AcceptableInteractionDistance) {
                        DrainAgentsStack(agentComponent, interactorsStackComponent, agentStackComponent, ref stackDrainerComponent);
                    }
                }
            }
        }

        private void DrainAgentsStack(AgentComponent agentComponent, StackComponent interactorsStackComponent, StackComponent agentStackComponent, ref StackDrainerComponent stackDrainerComponent) {
            if (agentStackComponent.ObservableStack.Count == 0 || stackDrainerComponent.RequestedDrainAmount == 0) {
                return;
            }

            if (stackDrainerComponent.Collector.childCount < stackDrainerComponent.RequestedDrainAmount && agentStackComponent.ObservableStack.TryPop(out GameObject result)) {
                if (result.TryGetComponent(out BoxCollider collider))
                {
                    stackDrainerComponent.SpotHeightDelta = collider.size.y * result.transform.localScale.z * stackDrainerComponent.SpotHeightDeltaMultiplier;
                    agentComponent.SpotHeightDelta = collider.size.y * result.transform.localScale.z * agentComponent.SpotHeightDeltaMultiplier;
                    Debug.Log(agentComponent.SpotHeightDelta);
                    Debug.Log(result.transform.localScale.z);
                }
                else
                {
                    stackDrainerComponent.SpotHeightDelta = agentComponent.DefaultSpotHeightDelta;
                    agentComponent.SpotHeightDelta = agentComponent.DefaultSpotHeightDelta;
                }
                
                result.transform.SetParent(stackDrainerComponent.Collector, true);
                result.transform.DOMove(stackDrainerComponent.AvailableProductItemSpot.position, 0.5f)
                    .OnComplete(() => { 
                        result.transform.DOKill();
                        interactorsStackComponent.ObservableStack.Push(result);
                    });
            
                HeightDeltaHandler.IncreaseSpotPosition(stackDrainerComponent.AvailableProductItemSpot, stackDrainerComponent.SpotHeightDelta);
                HeightDeltaHandler.DecreaseSpotPosition(agentComponent.AvailableStackSpot, agentComponent.SpotHeightDelta);
            }
        }
    }
}