using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;

    void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
        }
    }

    public void OnPlayClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    public void OnOptionsClicked()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }

        Debug.Log("Options clicked");
    }

    public void OnCloseSettingsClicked()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    public void OnToggleFullscreenClicked()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Fullscreen toggled");
    }

    public void OnVolumeChanged(float newVolume)
    {
        AudioListener.volume = newVolume;
        Debug.Log("Volume changed to: " + newVolume);
    }

    public void OnQuitClicked()
    {
        Debug.Log("Quit clicked");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}