using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Toggle vibrationToggle;
    public Toggle screenshakeToggle;

    void Start()
    {
        settingsPanel.SetActive(false); 

        
        vibrationToggle.isOn = PlayerPrefs.GetInt("Vibration", 1) == 1;
        screenshakeToggle.isOn = PlayerPrefs.GetInt("Screenshake", 1) == 1;

        
        vibrationToggle.onValueChanged.AddListener(OnVibrationToggle);
        screenshakeToggle.onValueChanged.AddListener(OnScreenShakeToggle);
    }

    public void OpenMenu()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
    }

    void OnVibrationToggle(bool isOn)
    {
        PlayerPrefs.SetInt("Vibration", isOn ? 1 : 0);
    }

    void OnScreenShakeToggle(bool isOn)
    {
        PlayerPrefs.SetInt("Screenshake", isOn ? 1 : 0);
    }
}


