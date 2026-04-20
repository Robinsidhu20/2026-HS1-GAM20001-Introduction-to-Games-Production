using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIController : MonoBehaviour
{
    public TMP_Text titleText;

    public void OnPlayClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    public void OnOptionsClicked()
    {
        if (titleText != null)
        {
            titleText.text = "Options clicked";
        }

        Debug.Log("Options clicked");
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
