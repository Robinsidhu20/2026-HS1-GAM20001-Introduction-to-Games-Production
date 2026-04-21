using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;

    public AudioSource sfxSource;
    public AudioClip buttonClickClip;

    void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
        }
    }

    void PlayButtonClickSound()
    {
        if (sfxSource != null && buttonClickClip != null)
        {
            sfxSource.PlayOneShot(buttonClickClip);
        }
    }

    public void OnPlayClicked()
    {
        PlayButtonClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    public void OnOptionsClicked()
    {
        PlayButtonClickSound();

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }

        Debug.Log("Options clicked");
    }

    public void OnCloseSettingsClicked()
    {
        PlayButtonClickSound();

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    public void OnToggleFullscreenClicked()
    {
        PlayButtonClickSound();
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
        PlayButtonClickSound();
        Debug.Log("Quit clicked");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}