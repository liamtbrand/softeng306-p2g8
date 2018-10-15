using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class HighScore{
    public string Name;
    public double Score;

    public HighScore(string name, double score){
        Name = name;
        Score = score;
    }
}