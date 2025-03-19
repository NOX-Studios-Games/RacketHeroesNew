using UnityEngine;

public class ShootAtPlayer : MonoBehaviour {
    public GameObject bullet;
    public Transform bulletPos;
    public float fireRate;
    private float currentTime;
    public float ballSpeed;

    private void Start() {
        currentTime = Random.Range(0, fireRate/2);
    }

    void Update() {
        if (GameManager.instance.gameStarted == false) return;
        currentTime += Time.deltaTime;
        if (currentTime >= fireRate) {
            currentTime = 0;
            Shoot();
        }
    }

    void Shoot() {
        var newBullet = Instantiate(bullet, bulletPos.position, Quaternion.identity);

        Vector3 dir = (GameManager.instance.player.transform.position - transform.position).normalized;

        newBullet.GetComponent<Rigidbody>().AddForce(new Vector3(dir.x,0, dir.z) * ballSpeed, ForceMode.Impulse);
    }
}