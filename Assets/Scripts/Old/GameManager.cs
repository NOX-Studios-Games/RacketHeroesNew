using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public List<Stage> stages = new();
    public Transform stagePosition;
    int currentStageCount = 0;
    GameObject currentStage;

    [Header("Components")]
    public static GameManager instance;
    public PlayerController player;
    public Ball ball;

    [Header("UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI stageNameText;
    public FixedJoystick fixedjoystick;

  [Header("Values")]
    public float timeUntilGameBegin;
    private float currentTime;
    public bool gameStarted;

    Vector3 direction;

    public UnityEvent startEvent;

    private void Awake() {
        instance = this;
    }

    public void Start() {
        NextStage();
        fixedjoystick.endMovementEvent.AddListener(() => BallInitialLaunch());
    }

    public void Update() {
        if (gameStarted) currentTime += Time.deltaTime;
        direction = new Vector3(fixedjoystick.Horizontal, 0f, fixedjoystick.Vertical);
        if (direction.x == 0) direction.z = 1;

        timeText.text = FormatTime(currentTime);
    }

    void NextStage() {
        currentTime = 0;
        currentStage = Instantiate(stages[currentStageCount].stage, stagePosition.position, Quaternion.identity, stagePosition);
        stageNameText.text = stages[currentStageCount].stageName;
        currentStageCount++;
        currentStage.GetComponent<DestroyOnNoChild>().destroyEvent.AddListener(EndingStage);
        gameStarted = true;
        StartPlayer();
    }

    public void BallInitialLaunch() {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame() {
        fixedjoystick.gameObject.SetActive(false);
        ball.body.AddForce(direction * player.ballSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(timeUntilGameBegin);
        startEvent.Invoke();
        gameStarted = true;
    }

    public string FormatTime(float time) {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartPlayer() {
        player.enabled = true;
    }

    public void ResetScene() {
        SceneManager.LoadSceneAsync(0);
    }

    public void EndingStage() {
        gameStarted = false;
        currentStage.GetComponent<DestroyOnNoChild>().destroyEvent.RemoveListener(EndingStage);
        currentStage = null;
        Debug.Log("Congratulations");

        if (currentStageCount >= stages.Count) {
            Debug.Log("You Won!");
        } else {
            Invoke("NextStage", 1f);
        }
    }
}