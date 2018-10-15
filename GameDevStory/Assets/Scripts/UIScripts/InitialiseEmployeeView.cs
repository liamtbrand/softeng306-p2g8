using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DialogueScripts;

public class InitialiseEmployeeView : MonoBehaviour {

    public NPCInfo npcInfo { get; set; }

    // Read only's
    private readonly float SLIDER_MAX_VALUE = 100;
    private readonly int FIRING_PENALTY = 100;

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
        var stats = npcInfo.Stats; // the randomly generated stats
        var attributes = npcInfo.Attributes; // the pre-made NPC attributes

        //spriteImage.sprite = stats.sprite;
        nameHeader.text = attributes.npcName;
        ageHeader.text = attributes.age.ToString();
        genderHeader.text = attributes.gender.ToString();
        bioBox.text = attributes.biography;
        costHeader.text = attributes.ammountPaidFor != 0 ? "$" + attributes.ammountPaidFor : "N/A";

        animator.runtimeAnimatorController = npcInfo.Attributes.animationController;

        // Initialise sliders
        FillSlider(communicationSlider, stats.Communication);
        FillSlider(testingSlider, stats.Testing);
        FillSlider(technicalSlider, stats.Technical);
        FillSlider(creativitySlider, stats.Creativity);
        FillSlider(designSlider, stats.Design);
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

    public void FireClicked()
    {
        FireEmployeeDialogue();
    }

    private void FireEmployeeDialogue()
    {
        var Dialogue = new Dialogue()
        {
            Sentences = new Sentence[]
            {
                new Sentence()
                {
                    icon = npcInfo.Attributes.headshot,
                    Title = npcInfo.Attributes.npcName,
                    sentenceLine = string.Format("Wait, you're firing me? Are you sure? If you fire me I'm going to demand ${0} in reparations.", FIRING_PENALTY),
                    sentenceChoices = new string[]
                    {
                        "Actualy, don't worry about it.",
                        "Sorry, but we're going to have to let you go."
                    },
                    sentenceChoiceActions = new UnityAction[]
                    {
                        () => 
                        {
                            Debug.Log(string.Format("You chose not to fire {0}.", npcInfo.Attributes.npcName));
                        },
                        () =>
                        {
                            // TODO: Fire the employee :(
                        }
                    }
                }
            }
        };
        DialogueManager.Instance.StartDialogue(Dialogue);
    }
}
