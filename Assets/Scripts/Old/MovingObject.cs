using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class MovingObject : MonoBehaviour{
    public Rigidbody body;
    public List<Transform> patrolPoints = new();
    Transform nextPatrolPoint;
    public float moveSpeed;
    float distance;
    public float maxDistanceToChangeDirection;

    public void Start() {
        nextPatrolPoint = patrolPoints[0];
    }

    public void Update(){
        if (body == null) {
            Destroy(gameObject);
            return;
        }

        var direction = nextPatrolPoint.position - body.transform.position;
        body.linearVelocity = moveSpeed * direction.normalized;

        distance = Vector3.Distance(body.transform.position, nextPatrolPoint.position);
        if (distance <= maxDistanceToChangeDirection) ChangePatrolPoint();
    }

    public void ChangePatrolPoint() {
        if (nextPatrolPoint.position == patrolPoints[0].position)
            nextPatrolPoint = patrolPoints[1];
        else
            nextPatrolPoint = patrolPoints[0];
    }
}