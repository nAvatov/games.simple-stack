using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct StackDrainerComponent {
        [SerializeField] private Transform _collectorSpot;
        [SerializeField] private Transform _drainedItemsSpot;
        [SerializeField] private TMPro.TextMeshProUGUI _requestedAmountTMP; 
        [SerializeField] private int _maxRequestAmount;
        private int _requestedDrainAmount;
        public int RequestedDrainAmount {
            get {
                return _requestedDrainAmount;
            }
            set {
                _requestedDrainAmount = value;
                _requestedAmountTMP.SetText("ordered: " + value);
            }
        }
        public Vector3 AvaiableItemSpot {
            get {
                return _drainedItemsSpot.position;
            }

            set {
                _drainedItemsSpot.position = value;
            }
        }
        public Transform CollectorSpot => _collectorSpot;
        public int MaxRequestAmount => _maxRequestAmount;
    }
}