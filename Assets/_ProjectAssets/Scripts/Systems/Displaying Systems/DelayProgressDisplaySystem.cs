using Leopotam.Ecs;
using Components;
using UnityEngine;
using DG.Tweening;
using System;

sealed class DelayProgressDisplaySystem : IEcsRunSystem{
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<DelayProgressDisplayComponent, DelayComponent> _progressDisplayFilter = null;
    public void Run() {
        DisplayProgress();
    }

    private void DisplayProgress() {
        foreach(var entity in _progressDisplayFilter) {
            ref var progressDisplayComponent = ref _progressDisplayFilter.Get1(entity);
            ref var delayComponent = ref _progressDisplayFilter.Get2(entity);

            if (delayComponent.IsDisplayable) {
                progressDisplayComponent.ProgressBarImage.gameObject.SetActive(true);
                progressDisplayComponent.ProgressBarImage.fillAmount = delayComponent.TimerState/delayComponent.TimerDuration;
            } else {
                progressDisplayComponent.ProgressBarImage.gameObject.SetActive(false);
            }
        }
    }
}
