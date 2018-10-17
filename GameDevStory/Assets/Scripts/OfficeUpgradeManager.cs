using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OfficeUpgradeManager : MonoBehaviour
{
    public Button GarageButton;
    public Button NormalOfficeButton;
    public Button LargeOfficeButton;
    public GameObject OfficeUpgradeWindow;

    public Sprite UpgradeDisabled;
    public Sprite UpgradedAlready;
    public Sprite UpgradeNormal;

    public Button CheatButton;

    private const int NormalOfficePrice = 400;
    private const int LargeOfficePrice = 750;

    private int _cheatButtonCount = 0;

    private void Start()
    {
        Debug.Log("Start UpgradeManager");
        OfficeUpgradeWindow.SetActive(false);
        UpdateButtonStates();
        NormalOfficeButton.onClick.AddListener(delegate
        {
            LevelManager.Instance.SwitchToLevel(1);
            GameManager.Instance.changeBalance(-1 * NormalOfficePrice);
            UpdateButtonStates();
        });
        LargeOfficeButton.onClick.AddListener(delegate
        {
            LevelManager.Instance.SwitchToLevel(2);
            GameManager.Instance.changeBalance(-1 * LargeOfficePrice);
            UpdateButtonStates();
        });
        
        CheatButton.onClick.AddListener(delegate
        {
            _cheatButtonCount++;
            if (_cheatButtonCount > 10)
            {
                GameManager.Instance.changeBalance(5000);
            }
        });
        ClickShowUpgrade();
    }

    private void Update()
    {
        if (OfficeUpgradeWindow.activeSelf)
        {
            UpdateButtonStates();
        }
    }

    public void ClickShowUpgrade()
    {
        Debug.Log("Show UpgradeManager");
        OfficeUpgradeWindow.SetActive(true);
        ProjectManager.Instance.PauseProject();
        UpdateButtonStates();
    }

    public void CloseUpgradeBox()
    {
        OfficeUpgradeWindow.SetActive(false);
        ProjectManager.Instance.ResumeProject();
    }

    private void UpdateButtonStates()
    {
        switch (LevelManager.Instance.level)
        {
            case 0:
                // Garage
                GarageButton.interactable = false;
                GarageButton.GetComponent<Image>().sprite = UpgradedAlready;
                LargeOfficeButton.interactable = true;
                LargeOfficeButton.GetComponent<Image>().sprite = UpgradeNormal;
                NormalOfficeButton.interactable = true;
                NormalOfficeButton.GetComponent<Image>().sprite = UpgradeNormal;
                if (GameManager.Instance.MoneyBalance < NormalOfficePrice - 249)
                {
                    NormalOfficeButton.interactable = false;
                    NormalOfficeButton.GetComponent<Image>().sprite = UpgradeDisabled;
                    LargeOfficeButton.interactable = false;
                    LargeOfficeButton.GetComponent<Image>().sprite = UpgradeDisabled;
                } else if (GameManager.Instance.MoneyBalance < LargeOfficePrice)
                {
                    LargeOfficeButton.interactable = false;
                    LargeOfficeButton.GetComponent<Image>().sprite = UpgradeDisabled;
                }
                break;
            case 1:
                // SmallOffice
                GarageButton.interactable = false;
                NormalOfficeButton.interactable = false;
                LargeOfficeButton.interactable = true;
                LargeOfficeButton.GetComponent<Image>().sprite = UpgradeNormal;
                GarageButton.GetComponent<Image>().sprite = UpgradedAlready;
                NormalOfficeButton.GetComponent<Image>().sprite = UpgradedAlready;
                if (GameManager.Instance.MoneyBalance < LargeOfficePrice -249)
                {
                    LargeOfficeButton.interactable = false;
                    LargeOfficeButton.GetComponent<Image>().sprite = UpgradeDisabled;
                }
                break;
            case 2:
                // LargeOffice
                GarageButton.interactable = false;
                NormalOfficeButton.interactable = false;
                LargeOfficeButton.interactable = false;
                GarageButton.GetComponent<Image>().sprite = UpgradedAlready;
                NormalOfficeButton.GetComponent<Image>().sprite = UpgradedAlready;
                LargeOfficeButton.GetComponent<Image>().sprite = UpgradedAlready;
                break;
        }
    }
}