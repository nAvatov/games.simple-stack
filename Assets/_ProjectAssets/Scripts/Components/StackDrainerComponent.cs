using UnityEngine;
using System;
using UnityEngine.Serialization;

namespace Components {
    [Serializable]
    public struct StackDrainerComponent {
        [Header("Item Placement")]
        [SerializeField] private Transform _collector;
        [SerializeField] private Transform _availableProductSpot;
        [SerializeField] private Transform _availableMoneySpot;
        // Should be automatic
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
        public Transform AvailableProductItemSpot {
            get {
                return _availableProductSpot;
            }

            set {
                _availableProductSpot = value;
            }
        }
        
        public Transform AvailableMoneyItemSpot {
            get {
                return _availableMoneySpot;
            }

            set {
                _availableMoneySpot = value;
            }
        }
        public Transform Collector => _collector;
        public float HeightDelta => _spotHeightDelta;
        public int MaxRequestAmount => _maxRequestAmount;
    }
}