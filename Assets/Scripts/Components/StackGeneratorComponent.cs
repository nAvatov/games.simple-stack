using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct StackGeneratorComponent {
        [SerializeField] private int _stackGenerationTime;
        [SerializeField] private float _collectDistance;
        [SerializeField] private Transform _generationSpot;
        [SerializeField] private Transform _collectPoint;
        public int StackGenerationTime => _stackGenerationTime;
        public float CollectDistance => _collectDistance;
        public Transform GenerationSpot => _generationSpot;
        public Vector3 CollectPoint => _collectPoint.position;
    }
}

