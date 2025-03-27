using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace _ProjectAssets.Scripts.Systems
{
    sealed class StackSetupSystem : IEcsInitSystem {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<StackComponent> _stackFilter = null;
        private readonly EcsFilter<RewardComponent> _rewardFilter = null;

        public void Init() 
        {
            InitializeProductStack();
            InitializeRewardStack();
        }

        private void InitializeProductStack()
        {
            foreach(var entity in _stackFilter) {
                ref var stackComponent = ref _stackFilter.Get1(entity);
                stackComponent.ObservableStack = new ObservableStack<GameObject>();     
                stackComponent.ObservableStack.CreateStack();
            }
        }

        private void InitializeRewardStack()
        {
            foreach(var entity in _rewardFilter) {
                ref var rewardComponent = ref _rewardFilter.Get1(entity);
                rewardComponent.ObservableStack = new ObservableStack<GameObject>();     
                rewardComponent.ObservableStack.CreateStack();
            }
        }
    }
}
