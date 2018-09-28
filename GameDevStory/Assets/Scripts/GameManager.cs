using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
    SPLASH_SCREEN, MAIN_MENU, PROJECT_SELECT, OFFICE, SCORE_SCREEN, CHECKPOINT_SCREEN, AD_SCREEN, SEND_JOB_SPLASH, HIRING_SCREEN, SHOP_SCREEN
}

public class GameManager : Singleton<GameManager> {

    private static bool created = false;

    private double moneyBalance;
    
    public GameScene gameScene;

    public Scenario[] scenarioArray;

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

    }

    public void switchScene(GameScene scene)
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
        gameScene = scene;
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
    void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {

        foreach(Scenario scenario in scenarioArray){

            if( Scenario.getActive() == false && Random.Range(0.0f, 1.0f) < scenario.GetScenarioProbability()){
                scenario.StartScenario();
            }

        }


	}
}
