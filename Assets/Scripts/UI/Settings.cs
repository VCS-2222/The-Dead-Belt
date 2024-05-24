using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioMixer masterMixer;

    public void ModifyAudioData()
    {
        masterMixer.SetFloat("volume", volumeSlider.value);
    }
}