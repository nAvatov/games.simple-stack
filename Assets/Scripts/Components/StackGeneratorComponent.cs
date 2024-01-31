using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct StackGeneratorComponent {
        [SerializeField] private uint _generationAmount;
        [SerializeField] private string _resourceName;
        [SerializeField] private int _generationAmountRestriction;
        [SerializeField] private GameObject _generationSpotsHolder;
        [SerializeField] private GameObject _generationCollector;
        public int NextPlacementPositionIndex;
        public uint GenerationAmount => _generationAmount;
        public string ResourceName => _resourceName;
        public Transform GenerationSpotsHolder => _generationSpotsHolder.transform;
        public Transform GenerationCollector => _generationCollector.transform;
        public int GenerationAmountRestriction => _generationAmountRestriction;
    }
}