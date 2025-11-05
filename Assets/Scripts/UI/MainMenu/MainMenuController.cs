using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Game.UI
{
    /// <summary>
    /// Simple main menu controller: hook this to UI Button OnClick events.
    /// Provides Play, Quit, and (optional) Credits handlers.
    /// </summary>
    public class MainMenuController : MonoBehaviour
    {
        [Header("Scene Flow")] [Tooltip("Name of the gameplay scene to load when Play is pressed.")] 
    public string gameplaySceneName = "Gameplay"; // Adjust to an existing scene name.
    [Tooltip("Name of the options scene to open when Options is pressed.")]
    public string optionsSceneName = "OptionsScene";

        [Header("Optional Panels")] public GameObject mainPanel; 
        public GameObject creditsPanel;

        [Header("Audio (Optional)")] public AudioSource uiAudioSource; 
        public AudioClip clickSound;

        [Header("Fade (Optional)")] public CanvasGroup fadeGroup; 
        public float fadeDuration = 0.5f;
        private bool _isTransitioning;

        private void Awake()
        {
            ShowMain();
            if (fadeGroup != null) fadeGroup.alpha = 0f; // start invisible -> fade in
        }

        private void Start()
        {
            if (fadeGroup != null) StartCoroutine(FadeRoutine(0f, 1f));
        }

        public void OnPlayClicked()
        {
            if (_isTransitioning) return;
            PlayClickSound();
            if (fadeGroup != null)
            {
                _isTransitioning = true;
                StartCoroutine(LoadAfterFade());
            }
            else
            {
                LoadGameplay();
            }
        }

        public void OnQuitClicked()
        {
            PlayClickSound();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void OnCreditsClicked()
        {
            PlayClickSound();
            if (creditsPanel != null) creditsPanel.SetActive(true);
            if (mainPanel != null) mainPanel.SetActive(false);
        }

        public void OnOptionsClicked()
        {
            if (_isTransitioning) return;
            PlayClickSound();
            if (string.IsNullOrWhiteSpace(optionsSceneName))
            {
                Debug.LogWarning("MainMenuController: optionsSceneName not set.");
                return;
            }
            if (!Application.CanStreamedLevelBeLoaded(optionsSceneName))
            {
                Debug.LogError($"MainMenuController: Scene '{optionsSceneName}' is not in build settings. Add it via File > Build Settings (or Build Profiles) -> 'Add Open Scenes'. Then retry.");
                return;
            }
            // Optionally fade out then load (reuse existing fade logic)
            if (fadeGroup != null)
            {
                _isTransitioning = true;
                StartCoroutine(LoadOptionsAfterFade());
            }
            else
            {
                SceneManager.LoadScene(optionsSceneName);
            }
        }

        private System.Collections.IEnumerator LoadOptionsAfterFade()
        {
            yield return FadeRoutine(1f, 0f);
            SceneManager.LoadScene(optionsSceneName);
        }

        public void OnBackFromCredits()
        {
            PlayClickSound();
            ShowMain();
        }

        private void ShowMain()
        {
            if (mainPanel != null) mainPanel.SetActive(true);
            if (creditsPanel != null) creditsPanel.SetActive(false);
        }

        private void LoadGameplay()
        {
            if (string.IsNullOrWhiteSpace(gameplaySceneName))
            {
                Debug.LogWarning("MainMenuController: gameplaySceneName not set.");
                return;
            }
            // Check if scene is included in Build Settings (or Build Profiles in newer Unity versions)
            if (!Application.CanStreamedLevelBeLoaded(gameplaySceneName))
            {
                Debug.LogError($"MainMenuController: Scene '{gameplaySceneName}' is not in build settings. Add it via File > Build Settings (or Build Profiles) -> 'Add Open Scenes'. Then retry.");
                return;
            }
            SceneManager.LoadScene(gameplaySceneName);
        }

        private System.Collections.IEnumerator LoadAfterFade()
        {
            yield return FadeRoutine(1f, 0f); // fade to black
            LoadGameplay();
        }

        private System.Collections.IEnumerator FadeRoutine(float start, float end)
        {
            if (fadeGroup == null) yield break;
            float t = 0f;
            fadeGroup.alpha = start;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                float lerp = Mathf.Clamp01(t / fadeDuration);
                fadeGroup.alpha = Mathf.Lerp(start, end, lerp);
                yield return null;
            }
            fadeGroup.alpha = end;
        }

        private void PlayClickSound()
        {
            if (uiAudioSource != null && clickSound != null)
            {
                uiAudioSource.PlayOneShot(clickSound);
            }
        }
    }
}
