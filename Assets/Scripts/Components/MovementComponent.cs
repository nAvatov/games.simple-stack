using UnityEngine;
using Voody.UniLeo;
using System;

namespace Components {
    [Serializable]
    public struct MovementComponent {
        public CharacterController CharacterController;
        public float MovementSpeed;
    }
}