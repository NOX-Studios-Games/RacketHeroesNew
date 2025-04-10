using Core.EventBus;
using Core.EventBus.Events;
using Core.Joystick;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

namespace Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private FloatingJoystick joystick;
        private Finger _movementFinger;
        public AudioClip sfxPlayerAttack;

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            EnhancedTouch.Touch.onFingerDown += HandleFingerDown;
            EnhancedTouch.Touch.onFingerUp += HandleFingerUp;
            EnhancedTouch.Touch.onFingerMove += HandleFingerMove;
        }

        private void OnDisable()
        {
            EnhancedTouch.Touch.onFingerDown -= HandleFingerDown;
            EnhancedTouch.Touch.onFingerUp -= HandleFingerUp;
            EnhancedTouch.Touch.onFingerMove -= HandleFingerMove;
            EnhancedTouchSupport.Disable();
        }

        private void HandleFingerDown(Finger touchedFinger)
        {
            if (_movementFinger != null) return;

            _movementFinger = touchedFinger;
            joystick.ShowJoystick(touchedFinger.screenPosition);
        }

        private void HandleFingerUp(Finger lostFinger) 
        {
            if (lostFinger != _movementFinger) return;

            _movementFinger = null;
            joystick.HideJoystick();
            EventBus<PlayerInputEvent>.Publish(new PlayerInputEvent(Vector2.zero));
            EventBus<PlayerAttackEvent>.Publish(new PlayerAttackEvent(sfxPlayerAttack));
        }
        
        private void HandleFingerMove(Finger movedFinger)
        {
            if (movedFinger != _movementFinger) return;

            var input = joystick.UpdateJoystick(movedFinger.screenPosition);
            EventBus<PlayerInputEvent>.Publish(new PlayerInputEvent(input));
        }
    }
}