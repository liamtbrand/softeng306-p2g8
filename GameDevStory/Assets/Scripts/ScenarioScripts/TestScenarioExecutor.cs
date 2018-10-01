using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DialogueScripts;

public class TestScenarioExecutor : AScenarioExecutor {

    public Sprite investorHead;

    public override void execute()
    {

        Debug.Log("Test scenario executing");

        UnityAction unityAction1 = TestAction1;

        UnityAction unityAction2 = TestAction2;

        UnityAction unityAction3 = TestAction3;

        Dialogue testDialogue = new Dialogue{
            Title = "Cameron",
            Sentences = new Sentence[]{
                new Sentence(){
                    sentenceLine = "Hi, I hope you are having a nice day"
                },
                new Sentence(){
                    sentenceLine = "Here are some choices",
                    sentenceChoices = new string[]{
                        "My first one",
                        "Another one!",
                        "And a third! :)"
                    },
                    sentenceChoiceActions = new UnityAction[]{
                        unityAction1,
                        unityAction2,
                        unityAction3
                    }
                }
            }
        };

        DialogueManager.Instance.StartDialogue(testDialogue, investorHead);

    }

    public void TestAction1(){
        Debug.Log("You chose choice 1!");
        DialogueManager.Instance.QueueDialogue(new Dialogue{
            Title = "Cameron",
            Sentences = new Sentence[]{
                new Sentence(){
                    sentenceLine = "Great choice, you chose choice 1!"
                }
            }
        });
    }

    public void TestAction2(){
        Debug.Log("You chose choice 2!");
        DialogueManager.Instance.QueueDialogue(new Dialogue{
            Title = "Cameron",
            Sentences = new Sentence[]{
                new Sentence(){
                    sentenceLine = "Great choice, you chose choice 2!"
                }
            }
        });
    }

    public void TestAction3(){
        Debug.Log("You chose choice 3!");
        DialogueManager.Instance.QueueDialogue(new Dialogue{
            Title = "Cameron",
            Sentences = new Sentence[]{
                new Sentence(){
                    sentenceLine = "Great choice, you chose choice 3!"
                }
            }
        });
    }

}
