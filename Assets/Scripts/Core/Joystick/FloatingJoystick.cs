using UnityEngine;

namespace Core.Joystick
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class FloatingJoystick : MonoBehaviour
    {
        [Header("Components")]
        public Canvas canvas;
        public RectTransform rectTransform;
        public RectTransform knob;
        
        [Header("Settings")]
        [SerializeField] private float joystickRadius = 150f;
        private float _maxMovement;
        private Canvas _parentCanvas;

        private void Awake() => ApplyJoystickSettings();

        private void ApplyJoystickSettings()
        {
            rectTransform.sizeDelta = Vector2.one * joystickRadius * 2f;
            _maxMovement = joystickRadius;
        }

        public void ShowJoystick(Vector2 screenPosition)
        {
            gameObject.SetActive(true);
            rectTransform.anchoredPosition = ClampStartPosition(ScreenToCanvasPosition(screenPosition));
        }

        public Vector2 UpdateJoystick(Vector2 screenPosition)
        {
            var localPosition = ScreenToCanvasPosition(screenPosition);
            var direction = Vector2.ClampMagnitude(localPosition - rectTransform.anchoredPosition, _maxMovement);
            knob.anchoredPosition = direction;
            
            return direction / _maxMovement;
        }

        public void HideJoystick()
        {
            knob.anchoredPosition = Vector2.zero;
            gameObject.SetActive(false);
        }
        
        private Vector2 ClampStartPosition(Vector2 startPosition)
        {
            var halfSize = joystickRadius;
            startPosition.x = Mathf.Clamp(startPosition.x, halfSize, Screen.width - halfSize);
            startPosition.y = Mathf.Clamp(startPosition.y, halfSize, Screen.height - halfSize);
            return startPosition;
        }

        private Vector2 ScreenToCanvasPosition(Vector2 screenPosition)
        {
            if (_parentCanvas == null) return screenPosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _parentCanvas.transform as RectTransform,
                screenPosition,
                _parentCanvas.worldCamera,
                out var localPosition
            );

            return localPosition;
        }
    }
}