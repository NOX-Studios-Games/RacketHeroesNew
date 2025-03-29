using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    [Header("Components")]
    public Rigidbody rb;
    public Animator anim;
    public FloatingJoystick joystick;
    public Transform hitCollider;

    [Header("Proprieties Variables")]
    public float moveSpeed;
    private Vector3 lastDirection;
    public bool autoHit;
    public LayerMask ballLayer;
    [Range(0,10)]
    public float ballSpeed;
    private int currentLife;
    public int maxLife;

    [Header("Animation Variables")]
    private bool isMoving;
    private bool isHitting;
    public float attackDuration;

    [Header("UI Variables")]
    public Slider ballSpeedSlider;
    public TextMeshProUGUI ballSpeedText;
    public Slider healtSlider;

    private void Awake() {
        if (ballSpeedSlider != null) ballSpeed = ballSpeedSlider.value;
        if (ballSpeedText != null) ballSpeedText.text = ballSpeed.ToString();
        if (healtSlider != null) healtSlider.maxValue = maxLife;
    }

    private void Start() {
        lastDirection = transform.forward;
        joystick.endMovementEvent.AddListener(() => EndMoviment());
        currentLife = maxLife;
        UpdateLife();
    }

    void FixedUpdate() {
        if(ballSpeedSlider != null) ballSpeed = ballSpeedSlider.value;
        if (ballSpeedText != null) ballSpeedText.text = ballSpeed.ToString();

        if (autoHit) {
            DetectBall(true);
        } else {
            if(isHitting) DetectBall(false);
        }
    }

    void Update() {
        MovePlayer();
        TurnPlayer();
        UpdateAnimationValues();
    }

    public void DetectBall(bool callAnimation) {
        Collider[] hitColliders = Physics.OverlapBox(hitCollider.position, hitCollider.localScale / 2, Quaternion.identity, ballLayer);
        if (hitColliders.Length == 0) return;
        if(callAnimation) CallHitAnimation();
        int i = 0;
        while (i < hitColliders.Length) {
            HitBall(hitColliders[i].GetComponent<Ball>());
            i++;
        }
    }

    public void HitBall(Ball ball) {
        ball.MoveBall(transform.eulerAngles.y, ballSpeed);
    }

    public void EnableAutoHit(bool auto) {
        autoHit = auto;
    }

    void MovePlayer() {
        Vector3 direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        rb.linearVelocity = direction * moveSpeed;
    }

    void TurnPlayer() {
        if (rb.linearVelocity != Vector3.zero) {
            lastDirection =  rb.linearVelocity.normalized;
        }

        transform.forward = lastDirection;
    }

    void UpdateAnimationValues() {
        isMoving = rb.linearVelocity.magnitude > 0.2f ? true : false;

        if(!isHitting) anim.SetBool("isMoving", isMoving);
    }

    void EndMoviment() {
        if (!autoHit) {
            CallHitAnimation();
            //DetectBall(false);
        }
    }

    void CallHitAnimation() {
        StartCoroutine(HitAnimation());
    }

    IEnumerator HitAnimation() {
        isHitting = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDuration);
        isHitting = false;
    }

    void UpdateLife() {
        healtSlider.value = currentLife;
    }

    void TakeDamage() {
        currentLife--;
        UpdateLife();
    }

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Enemy Projectile")) {
            TakeDamage();
        }
    }
}