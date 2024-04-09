using System;
using UnityEngine;

namespace Components {
    [Serializable]
    public struct DelayComponent {
        [SerializeField] public float TimerDuration;
        public float TimerState;
        public bool IsTimerExpired;
        public bool IsDisplayable;
    }
}
