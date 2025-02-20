using Voody.UniLeo;
using UnityEngine;
using System.Collections.Generic;
using System;
namespace Components {
    [Serializable]
    public struct StackComponent {
        [SerializeField] public TMPro.TextMeshProUGUI StackAmountTMP;
        [SerializeField] public bool IsTitleNeeded;
        public ObservableStack<GameObject> ObservableStack;
    }
}