using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick {
    public UnityEvent startMovementEvent;
    public UnityEvent endMovementEvent;

    protected override void Start() {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData) {
        startMovementEvent.Invoke();

        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData) {
        endMovementEvent.Invoke();

        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}