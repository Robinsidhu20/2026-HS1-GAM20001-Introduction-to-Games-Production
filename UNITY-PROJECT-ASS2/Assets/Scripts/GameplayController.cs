using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayController : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject coroutinePrefab;
    public GameObject countdownPrefab;
    public GameObject pauseCanvas;
    public TMP_Text pauseStatusText;

    private List<GameObject> createdObjects = new List<GameObject>();
    private bool isVisible = true;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                ToggleObjectVisibility();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                CreateCoroutineObject();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                CreateCountdownObject();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                DeleteLastCreatedObject();
            }
        }
    }

    void ToggleObjectVisibility()
    {
        if (targetObject != null)
        {
            isVisible = !isVisible;
            targetObject.SetActive(isVisible);
            Debug.Log("Toggled target object visibility");
        }
    }

    void CreateCoroutineObject()
    {
        if (coroutinePrefab != null)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-4f, 4f), Random.Range(-2f, 2f), 0f);
            GameObject newObject = Instantiate(coroutinePrefab, spawnPosition, Quaternion.identity);
            createdObjects.Add(newObject);
            Debug.Log("Created coroutine self-destruct object");
        }
    }

    void CreateCountdownObject()
    {
        if (countdownPrefab != null)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-4f, 4f), Random.Range(-2f, 2f), 0f);
            GameObject newObject = Instantiate(countdownPrefab, spawnPosition, Quaternion.identity);
            createdObjects.Add(newObject);
            Debug.Log("Created countdown self-destruct object");
        }
    }

    void DeleteLastCreatedObject()
    {
        if (createdObjects.Count > 0)
        {
            GameObject lastObject = createdObjects[createdObjects.Count - 1];
            createdObjects.RemoveAt(createdObjects.Count - 1);

            if (lastObject != null)
            {
                Destroy(lastObject);
                Debug.Log("Deleted last created object");
            }
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;

        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(isPaused);
        }

        if (pauseStatusText != null && !isPaused)
        {
            pauseStatusText.text = "";
        }

        if (isPaused)
        {
            Time.timeScale = 0f;
            Debug.Log("Game paused");
        }
        else
        {
            Time.timeScale = 1f;
            Debug.Log("Game resumed");
        }
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(false);
        }

        if (pauseStatusText != null)
        {
            pauseStatusText.text = "";
        }

        Time.timeScale = 1f;
        Debug.Log("Game resumed from button");
    }

    public void OnOptionsClicked()
    {
        if (pauseStatusText != null)
        {
            pauseStatusText.text = "Options clicked";
        }

        Debug.Log("Pause menu options clicked");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit clicked");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}