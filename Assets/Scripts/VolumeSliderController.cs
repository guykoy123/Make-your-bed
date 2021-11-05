using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSliderController : MonoBehaviour
{
    public enum VolumeType
    {
        master,
        music,
        gameSound
    }
    public VolumeType sliderType;
    Slider volumeSlider;
    public AudioMixer mixer;

    
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider = gameObject.GetComponent<Slider>();
        switch (sliderType)
        {
            case VolumeType.master:
                volumeSlider.value = OptionsData.masterVolume;
                break;
            case VolumeType.music:
                volumeSlider.value = OptionsData.musicVolume;
                break;
            case VolumeType.gameSound:
                volumeSlider.value = OptionsData.gameSoundVolume;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateMasterVolume(float sliderValue)
    {
        OptionsData.masterVolume = volumeSlider.value;
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }

    public void updateMusicVolume(float sliderValue)
    {
        OptionsData.musicVolume = volumeSlider.value;
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void updateGameSoundVolume(float sliderValue)
    {
        OptionsData.gameSoundVolume = volumeSlider.value;
        mixer.SetFloat("GameSoundVol", Mathf.Log10(sliderValue) * 20);
    }
}
