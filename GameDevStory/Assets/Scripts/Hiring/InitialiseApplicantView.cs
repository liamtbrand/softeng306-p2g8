using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InitialiseApplicantView : MonoBehaviour
{
    // Read only's
    private readonly float SLIDER_MAX_VALUE = 100;
    private readonly float VALUE = 60;

    // NPC to be displayed
    public GameObject npc;

    // Misc. character info
    public Image spriteImage;
    public TextMeshProUGUI nameHeader;
    public TextMeshProUGUI ageHeader;
    public TextMeshProUGUI genderHeader;
    public TextMeshProUGUI bioBox;

    // Skill bars
    public Slider communicationSlider;
    public Slider testingSlider;
    public Slider technicalSlider;
    public Slider creativitySlider;
    public Slider designSlider;

    // Use this for initialization
    void Start()
    {
        // Get the npc's stat from their stats script
        var stats = npc.GetComponent<CharacterStats>();

        spriteImage.sprite = stats.sprite;
        nameHeader.text = stats.name;
        ageHeader.text = stats.age.ToString();
        genderHeader.text = stats.gender.ToString();
        bioBox.text = stats.bio;

        // Initialise sliders
        FillSlider(communicationSlider, stats.communicationStat);
        FillSlider(testingSlider, stats.testingStat);
        FillSlider(technicalSlider, stats.technicalStat);
        FillSlider(creativitySlider, stats.creativityStat);
        FillSlider(designSlider, stats.designStat);
    }

    void Update()
    {
        Start();
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
