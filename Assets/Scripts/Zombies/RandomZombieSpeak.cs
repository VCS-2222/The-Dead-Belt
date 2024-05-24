using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomZombieSpeak : MonoBehaviour
{
    [Header("Needed Components")]
    [SerializeField] AudioClip[] voiceLines;
    [SerializeField] AudioSource zombieMouth;

    public void PlayRandomVoiceline()
    {
        int ranNum = Random.Range(0,voiceLines.Length);

        zombieMouth.PlayOneShot(voiceLines[ranNum]);
    }
}