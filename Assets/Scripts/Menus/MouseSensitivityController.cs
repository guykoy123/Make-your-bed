using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityController : MonoBehaviour
{
    Slider mouseSlider;

    // Start is called before the first frame update
    void Start()
    {
        mouseSlider = gameObject.GetComponent<Slider>();
        mouseSlider.value = OptionsData.mouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateSensetivity(float sliderValue)
    {
        OptionsData.mouseSensitivity = sliderValue;
    }
}
