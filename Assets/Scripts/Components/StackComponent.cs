using Voody.UniLeo;
using UnityEngine;
using System.Collections.Generic;
using System;
namespace Components {
    [Serializable]
    public struct StackComponent {
        [SerializeField] private TMPro.TextMeshProUGUI _amountTMP;
        public Stack<GameObject> Stack;
        public string StackAmount {
            set {
                _amountTMP?.SetText("stacked: " + value);
            }
        }
    }
}