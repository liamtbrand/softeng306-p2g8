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
     * and decide whether employee will join the company.
     */
    public void Negotiate()
    {
        var offer = PaySlider.value; // Get the user's offer from the pay slider
        var offerThreshold = npc.Attributes.costThreshold;

        if (offer >= offerThreshold)
        {

        }
        else
        {

        }
    }
}
