using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using DialogueScripts;
using UnityEngine.Events;

public class Negotiator : MonoBehaviour
{
    // TODO: deduct actuall offered ammount of money when purchasing
    // TODO: re-enable hire button after hire.
    // TODO: disable grid entry of npc after hire.
    public static NPCInfo npc;
    public static Button ClickedTile;

    public Slider PaySlider;
    public TextMeshProUGUI CostDisplay;
    public TextMeshProUGUI NameHeader;
    public Button OfferButton;

    private int ChancesLeftToHire = 5;

    public void Start()
    {
        Reload();
    }

    public void Reload()
    {
        // Offer slider starts out at npc's optimum value.
        PaySlider.value = npc.Attributes.cost;
        PaySlider.maxValue = 2 * npc.Attributes.cost; // Dynamically assign max. offer
        NameHeader.text = npc.Attributes.npcName;
    }

    public void Update()
    {
        try
        {
            CostDisplay.text = PaySlider.value.ToString();
        } catch (NullReferenceException)
        {
            // Not sure why but CostDisply is set to null here despite this it works.
        }
        
    }

    /**
     * To be called when the user has chosen a value to offer to the applicant, compare with npc's threshold
     * and decide whether employee will join the company.
     */
    public void Negotiate()
    {
        Debug.Log(npc.Attributes.costThreshold);
        var offer = PaySlider.value; // Get the user's offer from the pay slider
        var offerThreshold = npc.Attributes.costThreshold;

        if (offer >= offerThreshold)
        {
            // Debugging
            Debug.Log(string.Format("Offer of {0} is sufficient, threshold was {1}", offer.ToString(), offerThreshold.ToString()));

            // Offer is sufficient, applicant can be hired.
            NPCController.Instance.HireEmployee(npc);
            GameManager.Instance.changeBalance(npc.Attributes.cost * -1);
            ClickedTile.interactable = false;
            OfferButton.interactable = false;

            AcceptOfferDialogue();
        }
        else
        {
            // Debugging
            Debug.Log(string.Format("Offer of {0} is NOT sufficient, threshold was {1}", offer.ToString(), offerThreshold.ToString()));

            DeclineOfferDialogue();
        }
    }

    private void AcceptOfferDialogue()
    {
        var Dialogue = new Dialogue()
        {
            Sentences = new Sentence[]
            {
                new Sentence()
                {
                    icon = npc.Attributes.headshot,
                    Title = npc.Attributes.npcName,
                    sentenceLine = "I've decided, I'm going to accept! I'm looking forward to joining the team.",
                    sentenceChoices = new string[]
                    {
                        "Great to have you on board!"
                    },
                    sentenceChoiceActions = new UnityAction[]
                    {
                        () => {
                            Debug.Log(string.Format("{0} has accepted your offer, they have now joined the team!", npc.Attributes.npcName));
                        }
                    }
                }
            }
        };
        DialogueManager.Instance.StartDialogue(Dialogue);
    }

    private void DeclineOfferDialogue()
    {
        var Dialogue = new Dialogue()
        {
            Sentences = new Sentence[]
            {
                new Sentence()
                {
                    icon = npc.Attributes.headshot,
                    Title = npc.Attributes.npcName,
                    sentenceLine = "I'm sorry, but I expect to be paid a bit more than that!",
                    sentenceChoices = new string[]
                    {
                        "Okay, let's talk."
                    },
                    sentenceChoiceActions = new UnityAction[]
                    {
                        () => {
                            Debug.Log(string.Format("{0} has rejected your offer, you have {1} chances left.", npc.Attributes.npcName, ChancesLeftToHire));
                        }
                    }
                }
            }
        };
        DialogueManager.Instance.StartDialogue(Dialogue);
    }
}
