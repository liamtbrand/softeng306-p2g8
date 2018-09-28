using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialiseSlider : MonoBehaviour {

    public Image Fill; // Assign the fill value for the slider in the editor

    private const float SLIDER_MAX_VALUE = 100;
    private const float VALUE = 60;

    // TODO: Change from const to file read value
	void Start()
    {

        Slider slider = gameObject.GetComponent<Slider>();
        slider.maxValue = SLIDER_MAX_VALUE;

        slider.value = VALUE;

        // Based on value from 0-100 the slider color will fall in agradient from
        // red - yellow - green.
        Fill.color =  new Color(0.01f * (100 - VALUE), 0.01f * VALUE, 0);
    }
}
