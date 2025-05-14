using _ProjectAssets.Scripts.Components;
using Leopotam.Ecs;
using Components;
using Components.Events;
using UnityEngine;
using DG.Tweening;


sealed class RequestedOrderFulfieldSystem : IEcsRunSystem, IEcsInitSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<StackDrainerComponent, StackComponent> _stackDrainersFilter = null;
    private System.Random _drainRandomizer;

    public void Init() {
        _drainRandomizer = new System.Random();
        
        foreach(var entity in _stackDrainersFilter) {
            ref var draingerComponent = ref _stackDrainersFilter.Get1(entity);
            draingerComponent.RequestedDrainAmount = 2;
        }
    }

    public void Run() {
        CheckClientOrderFulfilnes();
    }

    private void CheckClientOrderFulfilnes() {
        foreach(var entity in _stackDrainersFilter) {
            ref var drainerComponent = ref _stackDrainersFilter.Get1(entity);
            ref var drainerStackComponent = ref _stackDrainersFilter.Get2(entity);
            
            // Deal is done
            if (drainerStackComponent.ObservableStack.Count >= drainerComponent.RequestedDrainAmount) {
                RemoveClient(drainerComponent);

                ref var rewardComponent = ref _stackDrainersFilter.GetEntity(entity).Get<RewardEventComponent>();

                rewardComponent.RewardAmount = drainerComponent.RequestedDrainAmount;
                
                drainerStackComponent.ObservableStack.Stack.Clear();
                drainerComponent.RequestedDrainAmount = _drainRandomizer.Next(1, drainerComponent.MaxRequestAmount);
            }
        } 
    }
    
    
    // TODO : logic for other system..
    private void RemoveClient(StackDrainerComponent drainerComponent) {
        for(int i = drainerComponent.Collector.childCount - 1; i >= 0; i--) {
            GameObject.DestroyImmediate(drainerComponent.Collector.GetChild(i).gameObject);
            HeightDeltaHandler.DecreaseSpotPosition(drainerComponent.AvailableProductItemSpot, drainerComponent.SpotHeightDelta);
        }
    }
}