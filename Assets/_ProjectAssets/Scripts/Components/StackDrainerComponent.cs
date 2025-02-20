using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct StackDrainerComponent {
        [Header("Item Placement")]
        [SerializeField] private Transform _collector;
        [SerializeField] private Transform _avaiableSpot;
        [SerializeField] private float _spotHeightDelta;
        [Header("Request")]
        [SerializeField] private TMPro.TextMeshProUGUI _requestedAmountTMP; 
        [SerializeField] private int _maxRequestAmount;
        
        private int _requestedDrainAmount;
        public int RequestedDrainAmount {
            get {
                return _requestedDrainAmount;
            }
            set {
                _requestedDrainAmount = value;
                _requestedAmountTMP.SetText("Order: " + value);
            }
        }
        public Transform AvaiableItemSpot {
            get {
                return _avaiableSpot;
            }

            set {
                _avaiableSpot = value;
            }
        }
        public Transform Collector => _collector;
        public float HeightDelta => _spotHeightDelta;
        public int MaxRequestAmount => _maxRequestAmount;
    }
}