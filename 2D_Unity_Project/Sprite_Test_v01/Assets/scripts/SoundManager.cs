using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip jumpSound;
    public AudioClip moveSound;
    private AudioSource audioSource;

    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();

        if (volumeSlider == null)
        {
            Debug.LogError("Volume Slider not assigned in Inspector!");
            return;
        }

        // Configure slider properties
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;
        volumeSlider.wholeNumbers = false;
        volumeSlider.interactable = true;

        // Load saved volume before adding listener
        float volumeToSet = 1f;
        if(PlayerPrefs.HasKey("Volume"))
        {
            volumeToSet = PlayerPrefs.GetFloat("Volume", 1f);
        }

        // Set value without triggering listener
        volumeSlider.SetValueWithoutNotify(volumeToSet);
        AudioListener.volume = volumeToSet;

        // Add listener AFTER setting initial value
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
        
        Debug.Log("SoundManager initialized. Slider interactable: " + volumeSlider.interactable + 
                  " | Min: " + volumeSlider.minValue + " Max: " + volumeSlider.maxValue + 
                  " | Current value: " + volumeSlider.value);
    }

    public void ChangeVolume(float value)
    {
        if (volumeSlider != null)
        {
            AudioListener.volume = value;
            Debug.Log("Volume changed to: " + value);
            Save();
        }
    }

    private void Load()
    {
        if (volumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
            volumeSlider.SetValueWithoutNotify(savedVolume);
            AudioListener.volume = savedVolume;
        }
    }

    private void Save()
    {
        if (volumeSlider != null)
        {
            PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Removed input handling from here
    }

    public void PlayJumpSound()
    {
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    public void PlayMoveSound()
    {
        if (moveSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(moveSound);
        }
    }
}
