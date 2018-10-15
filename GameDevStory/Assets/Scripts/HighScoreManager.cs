using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HighScoreManager : Singleton<HighScoreManager> {
    public InputField NameField;
    public GameObject ScoreListParent;
    public GameObject ScoreEntryPrefab;
    public Text ScoreDisplay;
    
    private string savePath;
    
    [SerializeField]
    private double _score = 420; // temp value
    
    [SerializeField]
    private List<HighScore> _scores;

    protected HighScoreManager () {} // enforces singleton use
    
    private void Start()
    {
        savePath = Application.persistentDataPath + "/highscores.save";
        LoadHighScores();
        _score = GameManager.Instance.getBalance();
        Destroy(GameManager.Instance);
        ScoreDisplay.text = "$" + _score.ToString("f2");

        var sorted = from entry in _scores orderby entry.Score ascending select entry;

        foreach (var entry in sorted)
        {
            DisplayScore(entry.Name, entry.Score);
        }
    }

    public void AddNewScore()
    {
        _scores.Add(new HighScore(NameField.text, _score));
        SaveHighScores();
        DisplayScore(NameField.text, _score);
    }

    private void SaveHighScores()
    {
        var save = new HighScores(){
            HighScoreList = _scores
        };

        var binaryFormatter = new  BinaryFormatter();
        using (var fileStream = File.Create(savePath)){
            binaryFormatter.Serialize(fileStream, save);
        }

        Debug.Log("Saved high scores!");
    }

    private void LoadHighScores(){

        HighScores loadedScores;
        if(File.Exists(savePath)){
            var binaryFormatter = new BinaryFormatter();
            using (var fileStream = File.Open(savePath, FileMode.Open)){
                loadedScores = (HighScores)binaryFormatter.Deserialize(fileStream);
            }

            _scores = loadedScores.HighScoreList;
        }else{
            _scores = new List<HighScore>();
            Debug.Log("High score file does not exists, creating new scores list");
        }
        Debug.Log("High scores loaded!");
    }

    private void DisplayScore(string name, double score)
    {
        var scorePrefab = Instantiate(ScoreEntryPrefab, Vector3.zero, Quaternion.identity, ScoreListParent.transform);
        var text = scorePrefab.GetComponentsInChildren<Text>();
        text[0].text = name;
        text[1].text = "$" + score.ToString("f2");
    }
}
