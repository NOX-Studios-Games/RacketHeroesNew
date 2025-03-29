using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public Rigidbody body;
    float currentSpeedMultiplier = 1;
    public float speedMultiplier = 1.05f;
    public float maxSpeedMultiplier = 2.5f;
    bool speedUpBall;
    public float speedTime;
    float currentTime;
    public float timeUntilBallReset;
    float currentBallResetTime;
    public float speedMagnitude;

    private void Update() {
        speedMagnitude = body.linearVelocity.magnitude;

        if (speedUpBall) {
            currentTime += Time.deltaTime;
            if (currentTime >= speedTime) {
                speedUpBall = false;
                currentTime = 0;
                body.linearVelocity = body.linearVelocity / currentSpeedMultiplier;
                currentSpeedMultiplier = 1;
            }
        }
        currentBallResetTime += Time.deltaTime;
        if (currentBallResetTime > timeUntilBallReset) {
            currentBallResetTime = 0;
            ResetBall();
        }

        if (speedMagnitude < GameManager.instance.player.ballSpeed - speedMultiplier) ResetBall();
    }

    public void MoveBall(float angle, float ballSpeed) {
        body.linearVelocity = Vector3.zero;
        Vector3 dir = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
        body.AddForce(dir * ballSpeed * currentSpeedMultiplier, ForceMode.Impulse);
        currentSpeedMultiplier = currentSpeedMultiplier * speedMultiplier;
        if (currentSpeedMultiplier > maxSpeedMultiplier) currentSpeedMultiplier = maxSpeedMultiplier;
        currentTime = 0;
        currentBallResetTime = 0;
        speedUpBall = true;
    }

    public void ResetBall() {
        var newDirection = transform.position - GameManager.instance.player.transform.position;
        //body.AddForce(Vector3.forward * 10, ForceMode.Impulse);
        //body.AddForce(newDirection * speedMultiplier, ForceMode.Impulse);
        var dir = body.linearVelocity.normalized;
        body.linearVelocity = Vector3.zero;
        body.AddForce(dir * GameManager.instance.player.ballSpeed * speedMultiplier, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Obstacle") {
            currentBallResetTime = 0;
        }
    }

    private void OnCollisionExit(Collision col) {
        if (col.gameObject.tag == "Wall") {
            body.linearVelocity *= currentSpeedMultiplier;
            speedUpBall = true;
        }
    }
}