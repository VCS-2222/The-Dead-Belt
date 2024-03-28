using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInteractive : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] AudioSource targetAudioSource;
    [SerializeField] GameObject textCanvas;
    [SerializeField] string functionName;
    [SerializeField] bool bringsUpText;

    public string ReturnFunctionName()
    {
        return functionName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (functionName == "log" && targetAudioSource.isPlaying)
            {
                textCanvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            textCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (!targetAudioSource.isPlaying)
        {
            textCanvas.SetActive(false);
        }
    }

    public void StopAudioSource()
    {
        targetAudioSource.Stop();
    }

    public void PauseAudioSource()
    {
        targetAudioSource.Pause();
    }

    public void StartAudioSource()
    {
        targetAudioSource.Play();
        textCanvas.SetActive(true);
    }

    public void PlayCustomClip(AudioClip clip)
    {
        targetAudioSource.clip = clip;
        targetAudioSource.Play();
    }
}