using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    public void SetVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}