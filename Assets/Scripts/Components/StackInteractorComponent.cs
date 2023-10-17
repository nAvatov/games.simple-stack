using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct StackInteractorComponent {
        [SerializeField] private float _acceptableInteractionDistance;
        [SerializeField] private Transform _interactionPoint;
        public float AcceptableInteractionDistance => _acceptableInteractionDistance;
        public Vector3 InteractionPoint => _interactionPoint.position;
    }
}

