using UnityEngine;
using UnityEngine.Events;

public class DestroyOnNoChild : MonoBehaviour {
    public UnityEvent destroyEvent;
    private void OnTransformChildrenChanged() {
        if (transform.childCount == 0) {
            destroyEvent.Invoke();
            Destroy(gameObject);
        }
    }
}