using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InitialiseApplicantView : MonoBehaviour
{
    // Reference to npc to display from previous tile script.
    public static NPCInfo npcInfo;
    public static Button clickedTile;

    // Read only's
    private readonly float SLIDER_MAX_VALUE = 100;
    private readonly float VALUE = 60;

    public LevelManager levelManager;

    // Misc. character info
    public Animator animator;
    public TextMeshProUGUI nameHeader;
    public TextMeshProUGUI ageHeader;
    public TextMeshProUGUI genderHeader;
    public TextMeshProUGUI bioBox;
    public TextMeshProUGUI costHeader;

    // Skill bars
    public Slider communicationSlider;
    public Slider testingSlider;
    public Slider technicalSlider;
    public Slider creativitySlider;
    public Slider designSlider;

    // Use this for initialization
    void Start()
    {
        Reload();
    }

    public void Reload()
    {
        // Get the npc's stat from their stats script
        var stats = npcInfo.stats; // the randomly generated stats
        var attributes = npcInfo.attributes; // the pre-made NPC attributes

        //spriteImage.sprite = stats.sprite;
        nameHeader.text = attributes.npcName;
        ageHeader.text = attributes.age.ToString();
        genderHeader.text = attributes.gender.ToString();
        bioBox.text = attributes.biography;
        costHeader.text = "$" + attributes.cost.ToString();

        animator.runtimeAnimatorController = npcInfo.attributes.animationController;

        // Initialise sliders
        FillSlider(communicationSlider, stats.communication);
        FillSlider(testingSlider, stats.testing);
        FillSlider(technicalSlider, stats.technical);
        FillSlider(creativitySlider, stats.creativity);
        FillSlider(designSlider, stats.design);
    }

    public void BackClicked()
    {
        GameManager.Instance.switchScene(GameScene.GridView);
    }

    public void HireClicked() {
        levelManager.AddEmployeeToLevel(npcInfo);
        GameManager.Instance.changeBalance(npcInfo.attributes.cost*-1);
        clickedTile.interactable = false;
    }

    private void FillSlider(Slider slider, float value)
    {
        slider.maxValue = SLIDER_MAX_VALUE;
        slider.value = value;

        // Based on value from 0-100 the slider color will fall in agradient from
        // red - yellow - green.
        var fillArea = slider.fillRect.GetComponent<Image>();
        fillArea.color = new Color(0.01f * (100 - value), 0.01f * value, 0);
    }
}
