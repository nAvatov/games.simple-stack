using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _ProjectAssets.Scripts.Components {
    
    [Serializable]
    public struct StackDrainerComponent {
        [Header("Item Placement")]
        [SerializeField] private Transform _collector;
        [SerializeField] private Transform _availableProductSpot;
        [SerializeField] private Transform _availableMoneySpot;
        [Header("Items Spot Delta")] 
        [SerializeField] private float _defaultSpotHeightDelta;
        [FormerlySerializedAs("_spotHeightDeltaMultiplier")] [SerializeField] [Range(1f, 2f)] private float _spotHeightDeltaDeltaMultiplier;
        [Header("Request")]
        [SerializeField] private TMPro.TextMeshProUGUI _requestedAmountTMP; 
        [SerializeField] private int _maxRequestAmount;
        
        private int _requestedDrainAmount;
        private float _spotSpotHeightDelta;
        public int RequestedDrainAmount {
            get => _requestedDrainAmount;
            set {
                _requestedDrainAmount = value;
                _requestedAmountTMP.SetText("Order: " + value);
            }
        }
        public Transform AvailableProductItemSpot {
            get => _availableProductSpot;
            set => _availableProductSpot = value;
        }
        
        public Transform AvailableMoneyItemSpot {
            get => _availableMoneySpot;
            set => _availableMoneySpot = value;
        }
        public Transform Collector => _collector;

        public float SpotHeightDelta
        {
            get => _spotSpotHeightDelta;
            set => _spotSpotHeightDelta = value;
        }

        public float DefaultSpotHeightDelta => _defaultSpotHeightDelta;
        public float SpotHeightDeltaMultiplier => _spotHeightDeltaDeltaMultiplier;
        public int MaxRequestAmount => _maxRequestAmount;
    }
}