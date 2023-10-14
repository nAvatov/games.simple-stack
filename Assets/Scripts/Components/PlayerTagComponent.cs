using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct PlayerTagComponent {
        [SerializeField] private Transform _playersTransform;
        [SerializeField] int _collectingDelay;
        public Vector3 Position => _playersTransform.position;
        public int CollectingDelay => _collectingDelay;
    }
}