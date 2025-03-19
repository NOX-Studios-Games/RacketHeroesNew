using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour {
    public int life = 1;
    public string colisionTag;

    public void TakeDamage() {
        life--;
        if (life <= 0) Death();
    }

    public void Death() {
        Destroy(gameObject, 0.1f);
    }

    private void OnCollisionEnter(Collision col) {
        if(string.IsNullOrEmpty(colisionTag)) {
            TakeDamage();
        } else {
            if (col.gameObject.CompareTag(colisionTag)) {
                TakeDamage();
            }
        }        
    }
}