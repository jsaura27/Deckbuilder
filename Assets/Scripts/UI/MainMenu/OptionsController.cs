using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// TextMeshPro optional support
using TMPro;

namespace Game.UI
{
    // Simple options controller for the OptionsScene.
    public class OptionsController : MonoBehaviour
    {
        public Slider masterVolumeSlider;
        public Toggle fullscreenToggle;
    public Dropdown qualityDropdown;
    // If you use TextMeshPro dropdowns, assign this instead (or as well).
    public TMP_Dropdown tmpQualityDropdown;
        public Button backButton;

        private void Start()
        {
            // Initialize UI from current settings
            if (masterVolumeSlider != null) masterVolumeSlider.value = AudioListener.volume;
            if (fullscreenToggle != null) fullscreenToggle.isOn = Screen.fullScreen;
            // Initialize quality dropdown (support regular UI Dropdown and TextMeshPro TMP_Dropdown)
            int currentQuality = QualitySettings.GetQualityLevel();
            if (qualityDropdown != null)
            {
                qualityDropdown.value = currentQuality;
                qualityDropdown.onValueChanged.AddListener(v => QualitySettings.SetQualityLevel(v));
            }
            else if (tmpQualityDropdown != null)
            {
                tmpQualityDropdown.value = currentQuality;
                tmpQualityDropdown.onValueChanged.AddListener(v => QualitySettings.SetQualityLevel(v));
            }

            if (masterVolumeSlider != null) masterVolumeSlider.onValueChanged.AddListener(v => AudioListener.volume = v);
            if (fullscreenToggle != null) fullscreenToggle.onValueChanged.AddListener(v => Screen.fullScreen = v);
            if (backButton != null) backButton.onClick.AddListener(OnBackClicked);
        }

        private void OnBackClicked()
        {
            // Return to the main menu — assumes main menu scene is the previous one or named "testScene"
            // We'll simply load the previous scene if possible, otherwise load a default.
            if (Application.CanStreamedLevelBeLoaded("testScene"))
            {
                SceneManager.LoadScene("testScene");
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
