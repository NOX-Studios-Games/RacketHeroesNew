using UnityEngine;
using UnityEngine.UI;

namespace Core.Menu
{
    public class SettingsManager : MonoBehaviour
    {
        public GameObject settingsPanel;
        public Toggle vibrationToggle;
        public Toggle screenShakeToggle;

        private void Start()
        {
            settingsPanel.SetActive(false); 

        
            vibrationToggle.isOn = PlayerPrefs.GetInt("Vibration", 1) == 1;
            screenShakeToggle.isOn = PlayerPrefs.GetInt("Screenshake", 1) == 1;

        
            vibrationToggle.onValueChanged.AddListener(OnVibrationToggle);
            screenShakeToggle.onValueChanged.AddListener(OnScreenShakeToggle);
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

        private static void OnVibrationToggle(bool isOn) => PlayerPrefs.SetInt("Vibration", isOn ? 1 : 0);

        private static void OnScreenShakeToggle(bool isOn) => PlayerPrefs.SetInt("Screenshake", isOn ? 1 : 0);
    }
}