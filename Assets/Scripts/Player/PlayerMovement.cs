using Core.EventBus;
using Core.EventBus.Events;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody rigidBody;

        private float _movementSpeed;
        private Vector2 _movementInput;
        
        private EventBinding<SetupAttributesEvent> _setupAttributesBinding;
        private EventBinding<PlayerInputEvent> _playerInputBinding;

        private void OnEnable()
        {
            _setupAttributesBinding = new EventBinding<SetupAttributesEvent>(SetupAttributes);
            _playerInputBinding = new EventBinding<PlayerInputEvent>(OnMove);
            
            EventBus<SetupAttributesEvent>.Register(_setupAttributesBinding);
            EventBus<PlayerInputEvent>.Register(_playerInputBinding);
        }

        private void OnDisable()
        {
            EventBus<SetupAttributesEvent>.Unregister(_setupAttributesBinding);
            EventBus<PlayerInputEvent>.Unregister(_playerInputBinding);
        }

        private void FixedUpdate() => MovePlayer();

        private void SetupAttributes(SetupAttributesEvent eventData) => _movementSpeed = eventData.MovementSpeed;

        private void OnMove(PlayerInputEvent eventData) => _movementInput = eventData.MovementInput;

        private void MovePlayer()
        {
            if (_movementInput == Vector2.zero)
            {
                rigidBody.linearVelocity = Vector3.zero;
                return;
            }

            var movement = new Vector3(_movementInput.x, 0, _movementInput.y) * (_movementSpeed * Time.fixedDeltaTime);
            rigidBody.linearVelocity = movement;

            RotatePlayer(movement);
        }

        private void RotatePlayer(Vector3 movementDirection)
        {
            if (movementDirection == Vector3.zero) return;
            transform.forward = movementDirection;
        }
    }
}