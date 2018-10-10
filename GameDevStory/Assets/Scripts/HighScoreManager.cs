using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : Singleton<HighScoreManager> {
    public InputField NameField;
    public GameObject ScoreListParent;
    public GameObject ScoreEntryPrefab;
    public Text ScoreDisplay;
    
    private double _score = 420; // temp value
    
    private Dictionary<string, double> _scores = new Dictionary<string, double>();

    protected HighScoreManager () {} // enforces singleton use
    
    private void Start()
    {
        _score = GameManager.Instance.getBalance();
        ScoreDisplay.text = "$" + _score;
        var sorted = from entry in _scores orderby entry.Value ascending select entry;
        foreach (var entry in sorted)
        {
            DisplayScore(name, _score);
        }
    }

    public void AddNewScore()
    {
        _scores.Add(NameField.text, _score);
        DisplayScore(NameField.text, _score);
    }

    private void DisplayScore(string name, double score)
    {
        var scorePrefab = Instantiate(ScoreEntryPrefab, Vector3.zero, Quaternion.identity, ScoreListParent.transform);
        var text = scorePrefab.GetComponentsInChildren<Text>();
        text[0].text = name;
        text[1].text = "$" + score;
    }
}
