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
        try
        {
            CostDisplay.text = PaySlider.value.ToString();
        } catch (NullReferenceException e)
        {
            // Not sure why but CostDisply is set to null here, despite the fact that it works.
        }
        
    }

    /**
     * To be called when the user has chosen a value to offer to the applicant, compare with npc's threshold
     * and decuise whether employee will join the company.
     */
    public void Negotiate()
    {
        var offer = PaySlider.value; // Get the user's offer from the pay slider
        var cost = 90;

        // Here we want to set the threshold to be a random number slightly lower than or equal to the 
        // applicant's advertised cost. We subtract the absolute value generated from a gaussian 
        // distribution scaled by 10% of the applicant's advertised cost. For example if we have an
        // applicant that advertises at $90, then their threshold will be:
        //
        //                  90 - 9 * [abs. of rand. gaussian value]
        //
        // This allows for very unlikely cases where the threshold for the applicant is incredibly low,
        // however their threshold is more likely to be very slightly under their advertised cost.
        var threshold = Math.Ceiling(cost - (cost * 0.1 * Math.Abs(NextGaussian())));

        if (offer >= threshold)
        {
            Debug.Log("Accepted");
        }
        else
        {
            Debug.Log("Denied");
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
