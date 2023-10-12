using UnityEngine;

namespace Components {
    public struct PlayerTagComponent {
        [SerializeField] private TriggerEventProvider _triggerEventProvider;
        public bool IsStayingInTrigger => _triggerEventProvider.IsStaying;
        public Collider TriggeredCollider => _triggerEventProvider.OtherCollider;
    }
}