using UnityEngine;

public class BoarDen : MonoBehaviour {
    public GameObject boar;
    public Transform boarSpawnPosition;
    public float fireRate;
    private float currentTime;

    private void Start() {
        currentTime = Random.Range(0, fireRate / 2);
    }

    void Update() {
        if (GameManager.instance.gameStarted == false) return;
        currentTime += Time.deltaTime;
        if (currentTime >= fireRate) {
            currentTime = 0;
            var jewBoar = Instantiate(boar, boarSpawnPosition.position, Quaternion.identity, transform.parent);
        }
    }
}
