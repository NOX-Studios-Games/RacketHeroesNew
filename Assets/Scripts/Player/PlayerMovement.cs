using Core.EventBus.Events;
using RacketHeroes.Core.EventBus;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private EventBinding<SetupAttributesEvent> _setupAttributesBinding;
        
        private float _movementSpeed;

        private void OnEnable()
        {
            _setupAttributesBinding = new EventBinding<SetupAttributesEvent>(SetupAttributes);
            EventBus<SetupAttributesEvent>.Register(_setupAttributesBinding);
        }

        private void OnDisable() => EventBus<SetupAttributesEvent>.Unregister(_setupAttributesBinding);

        private void SetupAttributes(SetupAttributesEvent eventData) => _movementSpeed = eventData.MovementSpeed;
    }
}