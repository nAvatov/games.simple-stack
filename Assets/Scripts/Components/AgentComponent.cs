using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct AgentComponent {
        [SerializeField] private Transform _playersTransform;
        [SerializeField] private int _collectingDelay;
        [SerializeField] private int _collectingRestriction;
        public Vector3 Position => _playersTransform.position;
        public int CollectingDelay => _collectingDelay;
        public int CollectingRestriction => _collectingRestriction;
    }
}