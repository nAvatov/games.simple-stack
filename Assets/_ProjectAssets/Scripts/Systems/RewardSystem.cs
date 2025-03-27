using Components;
using Components.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace _ProjectAssets.Scripts.Systems
{
    public class RewardSystem : IEcsRunSystem, IEcsInitSystem {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<RewardEventComponent, StackGeneratorComponent, RewardComponent> _rewardEventsFilter = null;

        public void Init()
        {
        
        }
    
        public void Run()
        {
            ApplyFoundedRewards();
        }

        private void ApplyFoundedRewards()
        {
            foreach (var rewardEntity in _rewardEventsFilter)
            {
                ref var rewardEventComponent = ref _rewardEventsFilter.Get1(rewardEntity);
            

                SpawnCashBlock(rewardEntity);
            
                _rewardEventsFilter.GetEntity(rewardEntity).Del<RewardEventComponent>();
            }
        }

        private void SpawnCashBlock(int entityIndex)
        {
            Debug.Log("CashSpawned");
            ref var generatorComponent = ref _rewardEventsFilter.Get2(entityIndex);
            ref var rewardComponent = ref _rewardEventsFilter.Get3(entityIndex);
            
            Debug.Log(rewardComponent.ObservableStack.Count);
            Debug.Log(generatorComponent.AvaiableItemSpots.Count);
            
            if (rewardComponent.ObservableStack.Count >= generatorComponent.AvaiableItemSpots.Count) {
                // At this point we're not spawning new objects
                return; 
            }
            
            Transform spotTransform = generatorComponent.AvaiableItemSpots[generatorComponent.NextPlacementPositionIndex];
            
            var resourceRequest = Resources.LoadAsync(generatorComponent.ResourceName); // TODO
            var rewardObject = GameObject.Instantiate(resourceRequest.asset as GameObject, spotTransform, true);
            rewardComponent.ObservableStack.Push(rewardObject);
            
            rewardObject.transform.localPosition = new Vector3(0, 0, 0);
            
            rewardObject.transform.SetParent(generatorComponent.GenerationCollector, true);
            generatorComponent.NextPlacementPositionIndex++;
        }
    }
}
