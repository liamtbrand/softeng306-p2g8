using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameScene
{
    SPLASH_SCREEN,
    MAIN_MENU,
    PROJECT_SELECT,
    OFFICE,
    SCORE_SCREEN,
    CHECKPOINT_SCREEN,
    AD_SCREEN,
    SEND_JOB_SPLASH,
    ApplicantView,
    GridView,
    SHOP_SCREEN
}

public class GameManager : Singleton<GameManager> {

    private static bool created = false;
    private double moneyBalance;
    
    public GameScene gameScene;

    public Scenario[] scenarioArray;

    public GameObject fadePanel;

    public Image image;

    private LevelManager levelScript;
    private int level = 0; // Hardcoded to first level for now

    // Initialise game at splash screen
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }

        gameScene = GameScene.SPLASH_SCREEN;
        moneyBalance = 1000;

        levelScript = GetComponent <LevelManager>();
        levelScript.SetupScene(level);
    }

    void StartLevel(int level)
    {
        levelScript = GetComponent <LevelManager>();
        levelScript.SetupScene(level);
        gameScene = GameScene.OFFICE;
    }

    public void switchScene(GameScene scene)
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
        gameScene = scene;
    }

    public void fadeToBlack(){
        Debug.Log("Fading");
        fadePanel.SetActive(true);
        image.canvasRenderer.SetAlpha( 0.0f );
        image.CrossFadeAlpha( 1.0f, 2, false );

    }

    public void unfade(){

        
        fadePanel.SetActive(false);
    }

    public void changeBalance(double inc)
    {
        moneyBalance += inc;
    }

    public double getBalance()
    {
        return moneyBalance;
    }


    // Use this for initialization
    void Start () 
    {
		

	}
	
	// Update is called once per frame
	void Update () {

        foreach (Scenario scenario in scenarioArray)
        {

            if (scenario.getStatus() == ScenarioStatus.INCOMPLETE && Scenario.getActive() == false && Random.Range(0.0f, 1.0f) < scenario.GetScenarioProbability())
            {
                scenario.StartScenario();
            }

        }
    }
}
