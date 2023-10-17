using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct StackGeneratorComponent {
        [SerializeField] private int _generationAmount;
        [SerializeField] private int _stackGenerationTime; 
        [SerializeField] private Transform _generationSpot;
        public int StackGenerationTime => _stackGenerationTime;
        public Transform GenerationSpot => _generationSpot;
        public int GenerationAmount => _generationAmount;
    }
}