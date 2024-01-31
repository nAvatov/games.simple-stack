using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct AgentComponent {
        [Header("Item Placement")]
        [SerializeField] private Transform _playersTransform;
        [SerializeField] private Transform _collectedItemsPlacement;
        [SerializeField] private Transform _avaiableStackSpot;
        [SerializeField] private float _spotHeightDelta;
        [Header("Collect Properties")]
        [SerializeField] private int _collectingDelay;
        [SerializeField] private int _collectingRestriction;
        public Vector3 Position => _playersTransform.position;
        public Transform AvaiableStackSpot {
            get {
                return _avaiableStackSpot;
            }

            set {
                _avaiableStackSpot = value;
            }
        }
        public Transform CollectedItemsPlacement => _collectedItemsPlacement;
        public int CollectingDelay => _collectingDelay;
        public int CollectingRestriction => _collectingRestriction;
        public float SpotHeightDelta => _spotHeightDelta;
    }
}