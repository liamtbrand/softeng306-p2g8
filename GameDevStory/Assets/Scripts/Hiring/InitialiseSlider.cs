using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InitialiseSlider : MonoBehaviour {

    public Image Fill; // Assign the fill value for the slider in the editor

    private readonly float SLIDER_MAX_VALUE = 100;
    private readonly float VALUE = 60;

    void Start()
    {
        string filepath = new string[] { Application.dataPath, "Scripts", "Hiring", "jeremiah.json" }
                .Aggregate((x, y) => Path.Combine(x, y)); // Append all four path elements
        
        if (File.Exists(filepath))
        {
            string jsonData = File.ReadAllText(filepath);
            Debug.Log(jsonData);
        } else
        {
            Debug.Log("Path does not exist: " + filepath);
        }

        // Adjust slider values
        Slider slider = gameObject.GetComponent<Slider>();
        slider.maxValue = SLIDER_MAX_VALUE;

        slider.value = VALUE;

        // Based on value from 0-100 the slider color will fall in agradient from
        // red - yellow - green.
        Fill.color =  new Color(0.01f * (100 - VALUE), 0.01f * VALUE, 0);
    }
}
