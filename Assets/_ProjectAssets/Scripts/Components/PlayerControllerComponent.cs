using Voody.UniLeo;
using UnityEngine.UI;
using UnityEngine;
using System;

[Serializable]
public struct PlayerControllerComponent {
    [SerializeField] private FixedJoystick _joystick;

    public float HorizontalInput => _joystick.Horizontal;
    public float VerticalInput => _joystick.Vertical;
    
}