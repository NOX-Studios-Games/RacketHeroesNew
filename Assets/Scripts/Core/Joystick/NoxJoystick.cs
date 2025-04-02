using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Core.Joystick
{
    public class NoxJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [FormerlySerializedAs("joystickBackground")] [Header("Components")]
        public RectTransform joystickBase;
        public RectTransform joystickHandle;
        
        [Header("Settings")]
        public float moveThreshold = 0.5f;
        private Vector2 _startPosition;
        private Vector2 _inputDirection;
        private bool _isDragging;

        public Vector2 InputDirection => _inputDirection;

        private void Start() => joystickBase.gameObject.SetActive(false);

        public void OnPointerDown(PointerEventData eventData)
        {
            _startPosition = eventData.position;
            joystickBase.position = _startPosition;
            joystickBase.gameObject.SetActive(true);
            _isDragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            joystickHandle.localPosition = Vector2.zero;
            _inputDirection = Vector2.zero;
            joystickBase.gameObject.SetActive(false);
            _isDragging = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(!_isDragging) return;
            
            var touchPosition = eventData.position;
            var direction = touchPosition - _startPosition;
            
            // Limita a movimentação do handle dentro da base
            var maxRadius = joystickBase.sizeDelta.x / 2;
            direction = Vector2.ClampMagnitude(direction, maxRadius);

            joystickHandle.localPosition = direction;
            _inputDirection = direction.normalized;
        }
    }
}