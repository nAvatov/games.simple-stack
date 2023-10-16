using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct StackInteractorComponent {
        [SerializeField] private bool _isGenerator;
        [SerializeField] private float _acceptableInteractionDistance;
        [SerializeField] private Transform _interactionPoint;
        [Header("Generator")]
        [SerializeField] private int _generationAmount;
        [SerializeField] private int _stackGenerationTime;
        [SerializeField] private Transform _generationSpot;
        [Header("Drainer")]
        [SerializeField] private int _requestedDrainAmount;
        [SerializeField] private int _drainDuration; 
        public int StackGenerationTime => _stackGenerationTime;
        public float AcceptableInteractionDistance => _acceptableInteractionDistance;
        public Transform GenerationSpot => _generationSpot;
        public Vector3 InteractionPoint => _interactionPoint.position;
        public bool IsGenerator => _isGenerator;
        public int RequestedDrainAmount => _requestedDrainAmount;
        public int DrainDuration => _drainDuration;
        public int GenerationAmount => _generationAmount;
    }
}

