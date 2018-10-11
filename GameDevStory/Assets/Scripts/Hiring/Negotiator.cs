using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using DialogueScripts;
using UnityEngine.Events;

public class Negotiator : MonoBehaviour
{    
    // TODO: make max slider value not exceed max bank account value

    public static NPCInfo npc;
    public static Button ClickedTile;

    public GameObject HiringDisplay;

    public Slider PaySlider;
    public TextMeshProUGUI CostDisplay;
    public TextMeshProUGUI NameHeader;
    public Button OfferButton;
    public GameObject HiringDisplayManager;

    // This value represents the frustrationg of the employee when negotiating. The more offers that
    // are too low the higher the value will become. If offers are significantly too low the value will
    // increase faster.
    private const int EMPLOYEE_FRUSTRATION_THRESHOLD = 2; // starting at 0, users get '3 chances' bar extraordinarily low offers, in which case less.

    public void Start()
    {
        Reload();
    }

    public void Reload()
    {
        // Offer slider starts out at npc's optimum value.
        PaySlider.value = npc.Attributes.cost;
        PaySlider.maxValue = Math.Min(2 * npc.Attributes.cost, (int)GameManager.Instance.getBalance()); // Dynamically assign max. offer and ensure not more than player can afford
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
     * and decide whether employee will join the company. If the offer is lower than the applicants threshold,
     * the user may offer once again but the applicant's frustration level will have risen. If the frustration
     * is too high then the negotiations will end.
     */
    public void Negotiate()
    {
        Debug.Log(npc.Attributes.costThreshold);
        var offer = PaySlider.value; // Get the user's offer from the pay slider
        var offerThreshold = npc.Attributes.costThreshold;

        if (offer >= offerThreshold) // Offer is greater or equal to threshold
        {
            // Debugging
            Debug.Log(string.Format("Offer of {0} is sufficient, threshold was {1}", offer.ToString(), offerThreshold.ToString()));

            // Offer is sufficient, applicant can be hired.
            NPCController.Instance.HireEmployee(npc);
            GameManager.Instance.changeBalance(offer * -1);
            ClickedTile.interactable = false;

            AcceptOfferDialogue();
        }
        else // Offer is less that threshold
        {
            // Check whether frustration threshold has been met
            if (npc.Attributes.negotiationFrustration < EMPLOYEE_FRUSTRATION_THRESHOLD) 
            {
                // If threshold has not been met continue negotiating
                if (offer < 0.7 * offerThreshold)
                {
                    npc.Attributes.negotiationFrustration += 2;
                    Debug.Log(string.Format("Offer of {0} is FAR BELOW sufficient, threshold was {1}", offer.ToString(), offerThreshold.ToString()));
                }
                else
                {
                    npc.Attributes.negotiationFrustration += 1;
                    Debug.Log(string.Format("Offer of {0} is NOT QUITE sufficient, threshold was {1}", offer.ToString(), offerThreshold.ToString()));
                }

                DeclineOfferDialogue();
            }
            else // Applicant frustration threshold has been met
            {
                EndNegotiationDialogue();
            }
            
        }
    }

    /**
     * Display dialogue that shows applicants accepting.
     */
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
                    sentenceLine = RandomAcceptingSentence((int) PaySlider.value),
                    sentenceChoices = new string[]
                    {
                        "Great to have you on board!"
                    },
                    sentenceChoiceActions = new UnityAction[]
                    {
                        () => {
                            Debug.Log(string.Format("{0} has accepted your offer, they have now joined the team!", npc.Attributes.npcName));
                            npc.IsAvailableForHire = false;
                            HiringDisplay.GetComponent<HiringDisplayManager>().ShowHiringGrid();
                        }
                    }
                }
            }
        };
        DialogueManager.Instance.StartDialogue(Dialogue);
    }

    /**
     * Display dialogue that show applicants declining
     */
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
                    sentenceLine = RandomDecliningSentence((int) PaySlider.value, npc.Attributes.costThreshold),
                    sentenceChoices = new string[]
                    {
                        "Okay, let's talk."
                    },
                    sentenceChoiceActions = new UnityAction[]
                    {
                        () => { Debug.Log(string.Format("{0} has rejected your offer, their frustration is {1}/{2}.", npc.Attributes.npcName, npc.Attributes.negotiationFrustration, EMPLOYEE_FRUSTRATION_THRESHOLD)); }
                    }
                }
            }
        };
        DialogueManager.Instance.StartDialogue(Dialogue);
    }

    /**
     * Display dialogue that show applicants ending negotiations
     */
    private void EndNegotiationDialogue()
    {
        var Dialogue = new Dialogue()
        {
            Sentences = new Sentence[]
            {
                new Sentence()
                {
                    icon = npc.Attributes.headshot,
                    Title = npc.Attributes.npcName,
                    sentenceLine = "I'm sorry, But I've decided to persur other career options. Have a good day.",
                    sentenceChoices = new string[]
                    {
                        "Thank you for your time."
                    },
                    sentenceChoiceActions = new UnityAction[]
                    {
                        () => {
                            Debug.Log(string.Format("{0} has ended negotiations, their frustration is {1}/{2}.", npc.Attributes.npcName, npc.Attributes.negotiationFrustration, EMPLOYEE_FRUSTRATION_THRESHOLD));
                            npc.IsAvailableForHire = false;
                            HiringDisplay.GetComponent<HiringDisplayManager>().ShowHiringGrid();
                        }
                    }
                }
            }
        };
        DialogueManager.Instance.StartDialogue(Dialogue);
    }

    /**
     * Returns a random sentence that the npc would say regarding the offer of acceptance.
     */
    private string RandomAcceptingSentence(int Offer)
    {
        System.Random rnd = new System.Random();
        string[] Sentences =
        {
            string.Format("I think I can come to terms with ${0}. I will accept!", Offer),
            string.Format("${0}? We have a deal! Super excited to get going!", Offer),
            string.Format("Finally, I have a job! Can't wait to tell mum! ${0} big ones woohoo!", Offer),
            string.Format("I'd be very happy with ${0}. I'm willing to accept, thank you for your time.", Offer),
            string.Format("${0} is just wonderful! I'll start on Monday.", Offer)
        };

        return Sentences[rnd.Next(Sentences.Length)];
    }

    /**
     * Returns a random sentence for the npc to say declining the offer of paymemnt. Chooses from sets of responses determined by
     * the ammount offered to look as if they are responding to the ammounts like a real person would.
     */
    private string RandomDecliningSentence(int Offer, int ThresholdCost)
    {
        // Sentences to display if the offer is reasonable bu too low
        string[] CloseSentences = 
        {
            string.Format("${0}? I'm sorry, but I expect to be paid a bit more than that!", Offer),
            string.Format("Hmm... I'm not sure ${0} will be quite enough to support me, sorry.", Offer),
            string.Format("May we continue negotiating? I'd like to push for a little higher than ${0}.", Offer)
        };

        // Sentences to display if the offer is unreasonably low.
        string[] NotCloseSentences =
        {
            string.Format("${0}?? Thats insulting! I'm sorry but I'll have to ask for a lot more.", Offer),
            string.Format("Not even close I'm afraid. I'll beed a lot more than ${0} to consider.", Offer),
            string.Format("I'm not joking around! I'm serious about this opportunity but ${0} as an offer makes me worry.", Offer)
        };

        System.Random rnd = new System.Random(); ;
        if (Offer < 0.7 * ThresholdCost) // Offer is not even close to threshold
        {
            return NotCloseSentences[rnd.Next(NotCloseSentences.Length)];
        }
        else // Offer is reasonable
        {
            return CloseSentences[rnd.Next(CloseSentences.Length)];
        }
    }
}
