using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Negotiator : MonoBehaviour
{
    public static NPCInfo npc;

    private void Start()
    {
        Negotiate();
    }

    public void Negotiate()
    {
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
