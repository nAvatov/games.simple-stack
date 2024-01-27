using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct StackGeneratorComponent {
        [SerializeField] private uint _generationAmount;
        public uint GenerationAmount => _generationAmount;
    }
}