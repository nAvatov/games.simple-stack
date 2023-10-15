using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct PlayerTagComponent {
        [SerializeField] private Transform _playersTransform;
        [SerializeField] private int _collectingDelay;
        [SerializeField] private int _collectingAmount;
        public Vector3 Position => _playersTransform.position;
        public int CollectingDelay => _collectingDelay;
        public int CollectingAmount => _collectingAmount;
    }
}