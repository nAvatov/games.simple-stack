using Voody.UniLeo;
using UnityEngine;
using System.Collections.Generic;
using System;
namespace Components {
    [Serializable]
    public struct StackComponent {
        [SerializeField] private TMPro.TextMeshProUGUI _amountTMP;
        public ObservableStack<GameObject> ObservableStack;
        public string DisplayedStackAmount {
            set {
                _amountTMP?.SetText("stacked: " + value);
            }
        }
    }
}