using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Slider Sensitivity;
    public PlayerLook playerLook;
    public SniperScope sniperScope;
    public Toggle scopeToggle;

    private void Start()
    {
        SetSensitivity();
        SetVolume();
        ToggleScope();
    }

    public void SetVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }

    public void SetSensitivity()
    {
        float sensitivity = Sensitivity.value;
        sensitivity = Mathf.Clamp(sensitivity, 5f, 50f);
        playerLook.xSensitivity = sensitivity;
        playerLook.ySensitivity = sensitivity;
    }

    public void ToggleScope()
    {
        if (scopeToggle.isOn)
        {
            sniperScope.toggleScope = true;
        }
        else
        {
            sniperScope.toggleScope = false;
        }
    }
}