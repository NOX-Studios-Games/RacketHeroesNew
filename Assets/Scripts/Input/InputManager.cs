using UnityEngine;
using UnityEngine.EventSystems;

namespace Input
{
    public class InputManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public RectTransform joystickBackground;
        public RectTransform joystick;
        
        private Vector2 _inputVector;
        
        // Joystick é ativado quando o jogador clica no background
        public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);
        
        // Joystick é desativado quando o jogador solta o botão
        public void OnPointerUp(PointerEventData eventData)
        {
            _inputVector = Vector2.zero;
            joystick.anchoredPosition = Vector2.zero;
        }

        // Atualiza a posição do joystick
        public void OnDrag(PointerEventData eventData)
        {
            var dragPosition = eventData.position - (Vector2)joystickBackground.position;
            
            _inputVector = dragPosition.magnitude > joystickBackground.sizeDelta.x / 2 ?
                dragPosition.normalized : dragPosition / (joystickBackground.sizeDelta.x / 2f);
            
            joystick.anchoredPosition = _inputVector * (joystickBackground.sizeDelta.x / 2f);
        }
        
        // Retorna o vetor de entrada normalizado
        public Vector2 GetInputVector() => _inputVector;
    }
}