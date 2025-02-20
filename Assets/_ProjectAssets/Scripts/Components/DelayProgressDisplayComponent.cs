using System;
using UnityEngine;
using UnityEngine.UI;

namespace Components {
    [Serializable]
    public struct DelayProgressDisplayComponent {
        [SerializeField] private Image _progressBarImage;

        public Image ProgressBarImage => _progressBarImage;
    }
}