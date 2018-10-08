using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiring
{
    public class Negotiator : MonoBehaviour
    {
        public static NPCInfo npc;

        public void Negotiate()
        {
            Debug.Log(NextGaussian());
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
                v1 = 2.0f * Random.Range(0f, 1f) - 1.0f;
                v2 = 2.0f * Random.Range(0f, 1f) - 1.0f;
                s = v1 * v1 + v2 * v2;
            } while (s >= 1.0f || s == 0f);

            s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);

            return v1 * s;
        }
    }

}