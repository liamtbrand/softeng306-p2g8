using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    public MoneyCounterAnimator MoneyCounter;

    public Scenario[] scenarioArray;

    public GameObject fadePanel;

    public Image image;

    // Initialise game at splash screen
    void Awake()
    {
        /*if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }*/

        gameScene = GameScene.SPLASH_SCREEN;
        moneyBalance = 0;
        if (MoneyCounter != null)
        {
            MoneyCounter.Target = moneyBalance;
        }
        
        LevelManager.Instance.SwitchToFirstLevel();
    }

    void StartLevel(int level)
    {
        LevelManager.Instance.SwitchToLevel(level);
        gameScene = GameScene.OFFICE;
    }

    public void switchScene(GameScene scene)
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
        gameScene = scene;
    }

    public void fadeToBlack(){
        Debug.Log("Fading");
		Debug.Log(image);
        fadePanel.SetActive(true);
        //image.canvasRenderer.SetAlpha( 0.01f );
        image.CrossFadeAlpha( 1.0f, 2, false );

    }
    
    public void Unfade(){

		image.CrossFadeAlpha(0f, 2, false);
        fadePanel.SetActive(false);
    }

    public void changeBalance(double inc)
    {
        moneyBalance += inc;
        if (MoneyCounter != null)
        {
            MoneyCounter.Target = moneyBalance;
        }
    }

    public double getBalance()
    {
        return moneyBalance;
    }

    [Obsolete("Please see StaffDiversityManager.getDiversityScore() instead", true)]
    public float getDiversityScore(){
        
        int maleCount = 0;
        foreach( KeyValuePair<GameObject, NPCInfo> npcPair in NPCController.Instance.NpcInstances){
            
            NPCInfo npcInfo = npcPair.Value;

            if(npcInfo.Attributes.gender.Equals(NPCAttributes.Gender.MALE)){
                maleCount++;
            }
        }
        float ratio = ((float)maleCount)/((float)NPCController.Instance.NpcInstances.Count);
        float deltaFromBalance = Mathf.Abs(ratio - 0.5f);
        float deltaThreshold;
        if(NPCController.Instance.NpcInstances.Count < 5){
            deltaThreshold = 0.5f;
        }else if(NPCController.Instance.NpcInstances.Count < 7){
            deltaThreshold = 0.3f;
        }else if(NPCController.Instance.NpcInstances.Count < 11){
            deltaThreshold = 0.2f;
        }else{
            deltaThreshold = 0.15f;
        }
        float scalarScore = deltaFromBalance - deltaThreshold;

        if(scalarScore < 0){
            scalarScore = 0;
        }

        Debug.Log("Diversity score is: " + (1 - scalarScore));

        return 1 - scalarScore;

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
