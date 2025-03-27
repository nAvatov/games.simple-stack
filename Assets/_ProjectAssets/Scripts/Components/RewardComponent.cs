using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
    [Serializable]
    public struct RewardComponent
    {
        [SerializeField] public TMPro.TextMeshProUGUI RewardAmountTMP;
        [SerializeField] public bool IsTitleNeeded;
        public ObservableStack<GameObject> ObservableStack;
    }
}