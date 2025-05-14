using System;
using UnityEngine;

namespace _ProjectAssets.Scripts.Components {
    
    [Serializable]
    public struct AgentComponent {
        [Header("Item Placement")]
        [SerializeField] private Transform _agentTransform;
        [SerializeField] private Transform _collectedItemsPlacement;
        [SerializeField] private Transform _availableStackSpot;

        [Header("Items Spot Delta")] 
        [SerializeField] private float _defaultSpotHeightDelta;
        [SerializeField] [Range(1f, 2f)] private float _spotHeightDeltaMultiplier;
        [Header("Collect Properties")]
        [SerializeField] private int _collectingDelay;
        [SerializeField] private int _collectingRestriction;
        public Vector3 Position => _agentTransform.position;
        public Transform AvailableStackSpot {
            get => _availableStackSpot;
            set => _availableStackSpot = value;
        }
        
        private float _spotHeightDelta;
        public float SpotHeightDelta
        {
            get => _spotHeightDelta;
            set => _spotHeightDelta = value;
        }
        
        public float DefaultSpotHeightDelta => _defaultSpotHeightDelta;
        
        public float SpotHeightDeltaMultiplier => _spotHeightDeltaMultiplier;
        public Transform CollectedItemsPlacement => _collectedItemsPlacement;
        public int CollectingDelay => _collectingDelay;
        public int CollectingRestriction => _collectingRestriction;
    }
}