using UnityEngine;
using UnityEngine.UI;

public class UIFollowTarget : MonoBehaviour {
    public Transform followTarget;
    public Camera mainCamera;

    void Update() {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(followTarget.position);

        RectTransform canvasRect = transform.parent.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPosition,
            null,
            out Vector2 canvasPosition
            );
        GetComponent<RectTransform>().anchoredPosition = canvasPosition;
    }
}