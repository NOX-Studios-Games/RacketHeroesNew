using UnityEngine;

namespace Core.EventBus.Events
{
    public struct PlayerInputEvent : IEvent
    {
        public Vector2 MovementInput { get; }

        public PlayerInputEvent(Vector2 movementInput)
        {
            MovementInput = movementInput;
        }
    }
}