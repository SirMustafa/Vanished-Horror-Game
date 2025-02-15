using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SfxSlider;
    AudioManager audioManager;
    float volume;

    void Start()
    {
        audioManager = AudioManager.AudioInstance;        
    }

    public void SetMusicValue()
    {
        volume = Mathf.Log10(MusicSlider.value) * 20;
        audioManager.SetMusicVolume(volume);
    }
    public void SetSfxValue()
    {
        volume = Mathf.Log10(SfxSlider.value) * 20;
        audioManager.SetSfxVolume(volume);
    }
}
