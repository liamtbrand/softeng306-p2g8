using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager> {
    public InputField NameField;
    public GameObject ScoreListParent;
    public GameObject ScoreEntryPrefab;
    public Text ScoreDisplay;
    
    private int score = 420; // temp value
    
    private Dictionary<string, int> _scores = new Dictionary<string, int>();

    protected ScoreManager () {} // enforces singleton use
    
    private void Start()
    {
        // TODO: Actually get score
        ScoreDisplay.text = "$" + score;
        var sorted = from entry in _scores orderby entry.Value ascending select entry;
        foreach (var entry in sorted)
        {
            DisplayScore(name, score);
        }
    }

    public void AddNewScore()
    {
        _scores.Add(NameField.text, score);
        DisplayScore(NameField.text, score);
    }

    private void DisplayScore(string name, int score)
    {
        var scorePrefab = Instantiate(ScoreEntryPrefab, Vector3.zero, Quaternion.identity, ScoreListParent.transform);
        var text = scorePrefab.GetComponentsInChildren<Text>();
        text[0].text = name;
        text[1].text = "$" + score;
    }
}
