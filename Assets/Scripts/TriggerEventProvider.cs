using UnityEngine;

public class TriggerEventProvider: MonoBehaviour {
    private bool _isStaying;
    private Collider _otherCollider;
    public bool IsStaying => _isStaying;
    public Collider OtherCollider => _otherCollider;

    private void OnTriggerStay(Collider other) {
        _isStaying = true;
        _otherCollider = other;
    }

    private void OnTriggerExit(Collider other) {
        _isStaying = false;
    }
}