using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct AgentComponent {
        [SerializeField] private Transform _playersTransform;
        [SerializeField] private Transform _collectedItemsPlacement;
        [SerializeField] private Transform _avaiableStackSpot;
        [SerializeField] private int _collectingDelay;
        [SerializeField] private int _collectingRestriction;
        public Vector3 Position => _playersTransform.position;
        public Vector3 AvaiableStackSpot {
            get {
                return _avaiableStackSpot.position;
            }

            set {
                _avaiableStackSpot.position = value;
            }
        }
        public Transform CollectedItemsPlacement => _collectedItemsPlacement;
        public int CollectingDelay => _collectingDelay;
        public int CollectingRestriction => _collectingRestriction;
    }
}