using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialiseSlider : MonoBehaviour {

    private const float SLIDER_MAX_VALUE = 100;

	void Start()
    {
        Slider slider = gameObject.GetComponent<Slider>();
        slider.maxValue = SLIDER_MAX_VALUE;

        slider.value = 40;
    }
}
