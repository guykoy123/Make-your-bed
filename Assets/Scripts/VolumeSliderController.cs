using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider = gameObject.GetComponent<Slider>();
        volumeSlider.value = OptionsData.masterVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateMasterVolume()
    {
        OptionsData.masterVolume = volumeSlider.value;
    }
}
