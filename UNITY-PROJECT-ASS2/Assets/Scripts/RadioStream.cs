using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RadioStream : MonoBehaviour
{
    public string streamUrl = "https://live-radio01.mediahubaustralia.com/2FMW/mp3/";

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on this GameObject!");
            return;
        }

        StartCoroutine(PlayStream());
    }

    IEnumerator PlayStream()
    {
        Debug.Log("Starting ABC Classic stream...");

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(streamUrl, AudioType.MPEG))
        {
            ((DownloadHandlerAudioClip)www.downloadHandler).streamAudio = true;

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Stream failed: " + www.error);
                yield break;
            }

            AudioClip clip = DownloadHandlerAudioClip.GetContent(www);

            if (clip == null)
            {
                Debug.LogError("Clip is NULL (Unity cannot decode this stream)");
                yield break;
            }

            // DEBUG INFO
            Debug.Log("Clip loaded successfully!");
            Debug.Log("Length: " + clip.length);
            Debug.Log("Frequency: " + clip.frequency);
            Debug.Log("Channels: " + clip.channels);
            Debug.Log("Samples: " + clip.samples);

            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();

            Debug.Log("Playback started");
        }
    }
}