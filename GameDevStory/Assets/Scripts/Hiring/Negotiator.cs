using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Negotiator : MonoBehaviour
{
    public static NPCInfo npc;

    public Slider PaySlider;
    public TextMeshProUGUI CostDisplay;

    public void Start()
    {
        // Initialise slider, image and dialogue.
    }

    public void Update()
    {
        CostDisplay.text = PaySlider.value.ToString();
    }

    /**
     * To be called when the user has chosen a value to offer to the applicant, compare with npc's threshold
     * and decuise whether employee will join the company.
     */
    public void Negotiate()
    {
        var offer = PaySlider.value; // Get the user's offer from the pay slider
        int cost = 90;

        for (int i = 0; i < 20; i++)
        {
            var NewCost = Math.Ceiling(cost - (cost * 0.1 * Math.Abs(NextGaussian())));
            Debug.Log(NewCost);
        }
    }

    /**
     * Using the Marsaglia polar method to generate gaussian distributed numbers. Taken from 
     * https://www.alanzucconi.com/2015/09/16/how-to-sample-from-a-gaussian-distribution/
     */
    public static float NextGaussian()
    {
        float v1, v2, s;
        do
        {
            v1 = 2.0f * UnityEngine.Random.Range(0f, 1f) - 1.0f;
            v2 = 2.0f * UnityEngine.Random.Range(0f, 1f) - 1.0f;
            s = v1 * v1 + v2 * v2;
        } while (s >= 1.0f || s == 0f);

        s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);

        return v1 * s;
    }
}
