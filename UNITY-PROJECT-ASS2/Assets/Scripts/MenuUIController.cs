using UnityEngine;
using TMPro;

public class MenuUIController : MonoBehaviour
{
    public TMP_Text titleText;

    public void OnPlayClicked()
    {
        titleText.text = "Play button clicked";
    }

    public void OnOptionsClicked()
    {
        titleText.text = "Options button clicked";
    }

    public void OnQuitClicked()
    {
        titleText.text = "Quit button clicked";

        Debug.Log("Quit button clicked");

        Application.Quit();
    }
}
