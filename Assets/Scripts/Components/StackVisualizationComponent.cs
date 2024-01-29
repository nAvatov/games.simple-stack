using System;
using UnityEngine;

namespace Components {
    [Serializable]
    public struct StackVisualizationComponent {
        [SerializeField] private string _resourceName;
        [SerializeField] private int _generationChunkAmount;
        [SerializeField] private GameObject _generationSpotsHolder;
        [SerializeField] private GameObject _generationCollector;
        public bool IsVisualizationRequired;
        public int NextPlacementPositionIndex;
        public string ResourceName => _resourceName;
        public Transform GenerationSpotsHolder => _generationSpotsHolder.transform;
        public Transform GenerationCollector => _generationCollector.transform;
        public int GenerationChunkAmount => _generationChunkAmount;
    }
}