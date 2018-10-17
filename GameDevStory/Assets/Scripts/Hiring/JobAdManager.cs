using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class JobAdManager : MonoBehaviour {

    
    // The job advertisement screen to be shown
    public GameObject jobAdEditor;
    public GameObject sendButton;

    private List<string> feminineWords = new List<string>() {
        "community of engineers", 
        "family of coders",
        "teamwork",
        "communication",
        "flexibility",
        "transparency",
        "fostering meaningful relationships",
        "satisfied",
        "happy",
        "proficient",
        "experienced",
        "team",
        "collaborative",
        "natural",
        "solve",
        "passionate",
        "fantastic"};

    void Start() {
        jobAdEditor.SetActive(false);
    }

    // Display the job advertisement editor
    public void ShowEditor()
    {
        if (GameManager.Instance.getBalance() < 80) {
            sendButton.GetComponent<Button>().interactable = false;
            sendButton.GetComponent<Image>().color = new Color(194f/255f, 194f/255f, 194f/255f, 255f/255f);
        } else {
            sendButton.GetComponent<Button>().interactable = true;
            sendButton.GetComponent<Image>().color = new Color(66f/255f, 255f/255f, 74f/255f, 255f/255f);
        }
        jobAdEditor.SetActive(true);
    }

    // This should be called to start the first level
    public void CloseEditor()
    {
        jobAdEditor.SetActive(false);
    }

    public void Advertise() {

        // Pay for cost of advertising
        GameManager.Instance.changeBalance(-80);

        List<String> values = GetDropdownValues();
        int proportion = (int)(GetFemaleApplicantProportion(values)*10);

        int numberOfFemales = 0;
        if (proportion == 3 || proportion == 2)
        {
            numberOfFemales = 1;
        }
        else if (proportion == 4)
        {
            numberOfFemales = 1;
        }
        else if (proportion == 5)
        {
            numberOfFemales = 2;
        }
        else if (proportion == 6)
        {
            numberOfFemales = 2;
        }
        else if (proportion == 7)
        {
            numberOfFemales = 3;
        }
        else if (proportion >= 8)
        {
            numberOfFemales = 4;
        }

        NPCFactory.Instance.SetNumberOfFemales(numberOfFemales);

        Debug.Log("Proportion is: " + proportion);
    }

    public List<string> GetDropdownValues() 
    {
        List<string> selectedItems = new List<string>();
        Dropdown[] dropdownList = jobAdEditor.gameObject.GetComponentsInChildren<Dropdown>();

        foreach (Dropdown d in dropdownList) {
            int menuIndex = d.value;
            List<Dropdown.OptionData> menuOptions = d.options;
            selectedItems.Add(menuOptions[menuIndex].text);
        }

        return selectedItems;
    }

    public double GetFemaleApplicantProportion(List<string> values) {

        double proportion = 0;

        foreach (string selectedWord in values) {
            if (feminineWords.Contains(selectedWord)) {
                // Add 10% for each 'feminine' word selected
                proportion += 0.1;
            }
        }
        return proportion;
    }

}