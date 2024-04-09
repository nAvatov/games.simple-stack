using System;
using UnityEngine;
using Components;
using Leopotam.Ecs;

sealed class DelaySystem : IEcsRunSystem, IEcsInitSystem {
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<DelayComponent> _delayFilter = null;

    public void Init() {
        foreach(var entity in _delayFilter) {
            ref var delayComponent = ref _delayFilter.Get1(entity);

            delayComponent.IsDisplayable = false;
            delayComponent.TimerState = delayComponent.TimerDuration;
        }
    }

    public void Run() {
        foreach(var entity in _delayFilter) {
            ref var delayComponent = ref _delayFilter.Get1(entity);

            // This condition requiers manual TimerState update in referenced systems
            if (delayComponent.TimerState > 0f) {
                delayComponent.TimerState -= Time.deltaTime;
 
                if (delayComponent.TimerState <= 0f) {
                    delayComponent.IsTimerExpired = true;
                    delayComponent.IsDisplayable = false;
                    delayComponent.TimerState = -1f;
                }
            }
        }
    }
}