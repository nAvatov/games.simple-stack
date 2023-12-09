using System;
using UnityEngine;
using Components;
using Leopotam.Ecs;

sealed class DelaySystem : IEcsRunSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<DelayComponent> _delayFilter = null;
    public void Run() {
        foreach(var entity in _delayFilter) {
            ref var delayComponent = ref _delayFilter.Get1(entity);

            if (delayComponent.TimerState > 0f) {
                delayComponent.TimerState -= Time.deltaTime;
 
                if (delayComponent.TimerState <= 0f) {
                    delayComponent.IsTimerExpired = true;
                    delayComponent.TimerState = -1f;
                }
            }
        }
    }
}