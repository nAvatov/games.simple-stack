using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct StackGeneratorComponent {
        [SerializeField] private int _generationAmount;
        [SerializeField] private GameObject _generationSpotsHolder;
        [SerializeField] private GameObject _generationCollector;
        public int NextPlacementPositionIndex;
        public Transform GenerationSpotsHolder => _generationSpotsHolder.transform;
        public Transform GenerationCollector => _generationCollector.transform;
        public int GenerationAmount => _generationAmount;
    }
}