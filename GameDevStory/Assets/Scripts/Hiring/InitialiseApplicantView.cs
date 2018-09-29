using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InitialiseApplicantView : MonoBehaviour
{
    // Read only's
    private readonly float SLIDER_MAX_VALUE = 100;
    private readonly float VALUE = 60;

    // Character image
    public Sprite sprite;
    public Image spriteImage;

    // Skill bars
    public Slider communicationSlider;
    public Slider testingSlider;
    public Slider technicalSlider;
    public Slider creativitySlider;
    public Slider designSlider;

    // Use this for initialization
    void Start()
    {
        spriteImage.sprite = sprite;

        // Initialise sliders
        FillSlider(communicationSlider, 54);
        FillSlider(testingSlider, 78);
        FillSlider(technicalSlider, 89);
        FillSlider(creativitySlider, 23);
        FillSlider(designSlider, 34);
    }

    private void FillSlider(Slider slider, float value)
    {
        slider.maxValue = SLIDER_MAX_VALUE;
        slider.value = value;

        // Based on value from 0-100 the slider color will fall in agradient from
        // red - yellow - green.
        var fillArea = slider.fillRect.GetComponent<Image>();
        fillArea.color = new Color(0.01f * (100 - value), 0.01f * value, 0);
    }
}
