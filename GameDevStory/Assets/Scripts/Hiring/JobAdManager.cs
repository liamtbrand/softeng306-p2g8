using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class JobAdManager : MonoBehaviour {

    
    // The job advertisement screen to be shown
    public GameObject jobAdEditor;

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
        jobAdEditor.SetActive(true);
    }

    // This should be called to start the first level
    public void CloseEditor()
    {
        jobAdEditor.SetActive(false);
    }

    public void Advertise() {
        List<String> values = GetDropdownValues();
        double proportion = GetFemaleApplicantProportion(values);
        // TODO: Link this to percentage of female / male applicants in hiring grid
    }

    public List<string> GetDropdownValues() 
    {
        List<string> selectedItems = new List<string>();
        Dropdown[] dropdownList = this.gameObject.GetComponents<Dropdown>();

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