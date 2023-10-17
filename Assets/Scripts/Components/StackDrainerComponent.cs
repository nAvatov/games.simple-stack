using UnityEngine;
using System;

namespace Components {
    [Serializable]
    public struct StackDrainerComponent {
        [SerializeField] private TMPro.TextMeshProUGUI _requestedAmountTMP; 
        [SerializeField] private int _maxRequestAmount;
        [SerializeField] private int _drainDuration; 
        private int _requestedDrainAmount;
        public int RequestedDrainAmount {
            get {
                return _requestedDrainAmount;
            }
            set {
                _requestedDrainAmount = value;
                _requestedAmountTMP.SetText("ordered: " + value);
            }
        }
        public int DrainDuration => _drainDuration;
        public int MaxRequestAmount => _maxRequestAmount;
    }
}