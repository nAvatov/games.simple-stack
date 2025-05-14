using _ProjectAssets.Scripts.Components;
using Components;
using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace _ProjectAssets.Scripts.Systems.Interaction_Systems
{
    sealed class AgentsStackFillingSystem : IEcsRunSystem{
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<AgentComponent, StackComponent, DelayComponent> _agentStackFilter = null;
        private readonly EcsFilter<StackBrockerComponent, DelayComponent, StackComponent, StackGeneratorComponent> _stackInteractionFilter = null;
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
                            TryFillAgentsStack(agentComponent, generatorStackComponent, agentStackComponent, ref generatorComponent, ref agentLootDelayComponent);
                            generatorDelayComponent.TimerState = generatorDelayComponent.TimerDuration;
                        }
                    
                        agentLootDelayComponent.IsTimerExpired = false;
                        agentLootDelayComponent.TimerState = agentLootDelayComponent.TimerDuration;
                    }
                }
            }
        }

        private void TryFillAgentsStack(AgentComponent agentComponent, StackComponent interactorsStackComponent, StackComponent agentStackComponent, ref StackGeneratorComponent generatorComponent, ref DelayComponent delayComponent) {
            if (agentStackComponent.ObservableStack == null)
            {
                agentStackComponent.ObservableStack = new ObservableStack<GameObject>();
                agentStackComponent.ObservableStack.CreateStack();
            }

        
            // while loop here if multiple collecting at the same time is required
            if (agentStackComponent.ObservableStack.Count < agentComponent.CollectingRestriction && interactorsStackComponent.ObservableStack.Count > 0) {
                if (interactorsStackComponent.ObservableStack.TryPop(out GameObject result)) {
                    delayComponent.IsDisplayable = true;
                    agentStackComponent.ObservableStack.Push(result);
                    CollectItem(agentComponent, result);
                

                    generatorComponent.NextPlacementPositionIndex--;
                }
            }
        }

        private void CollectItem(AgentComponent agentComponent, GameObject item) {
            if (item.TryGetComponent(out BoxCollider collider))
            {
                agentComponent.SpotHeightDelta += collider.size.y * item.transform.localScale.z * agentComponent.SpotHeightDeltaMultiplier;
                Debug.Log(agentComponent.SpotHeightDelta);
                Debug.Log(item.transform.localScale.z);
            }
            else
            {
                agentComponent.SpotHeightDelta += agentComponent.DefaultSpotHeightDelta;
            }
            
            item.transform.SetParent(agentComponent.CollectedItemsPlacement);
            item.transform.DOLocalMove(agentComponent.AvailableStackSpot.localPosition, 0.5f);
        
            HeightDeltaHandler.IncreaseSpotPosition(agentComponent.AvailableStackSpot, agentComponent.SpotHeightDelta);
        }
    }
}