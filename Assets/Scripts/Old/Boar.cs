using UnityEngine;

public class Boar : MonoBehaviour {
    public Rigidbody rb;
    public float speed;
    private float directionCurve = 1;
    private Vector2 currentDirection;

    void FixedUpdate() {
        Move();
    }

    public void Move() {
        Vector3 dir = (GameManager.instance.player.transform.position - transform.position).normalized;
        currentDirection = Vector2.MoveTowards(currentDirection, dir, directionCurve * Time.deltaTime);
        
        rb.linearVelocity = currentDirection * speed;
    }
}