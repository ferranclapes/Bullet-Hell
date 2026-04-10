using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField] private GameObject levelUpScreen;
    [SerializeField] private TextMeshProUGUI fromToLevelText;

    [Header("Upgrade Texts")]
    [SerializeField] private TextMeshProUGUI upgrade1;
    [SerializeField] private TextMeshProUGUI upgrade2;
    [SerializeField] private TextMeshProUGUI upgrade3;

    [Header("Upgrade Icons")]
    [SerializeField] private Image upgrade1Icon;
    [SerializeField] private Image upgrade2Icon;
    [SerializeField] private Image upgrade3Icon;


    [SerializeField] private UpgradeManagerScript upgradeManager;
    
    void Start()
    {
        levelUpScreen.SetActive(false);
    }
    public void ShowLevelUpScreen(int newLevel)
    {
        Time.timeScale = 0f;
        fromToLevelText.text =  (newLevel-1).ToString() + " -> " + newLevel.ToString();

        List<UpgradeScript> selectedUpgrades = upgradeManager.Get3Upgrades();
        if (selectedUpgrades[0].upgradeType == UpgradeType.WeaponUpgrade)
        {
            upgrade1.text = "<b>"+ selectedUpgrades[0].upgradeName+"</b>: " + Player.Instance.weaponManager.GetLevelUpDescription(selectedUpgrades[0].weaponType);
        }
        else
        {
            upgrade1.text = "<b>"+ selectedUpgrades[0].upgradeName+"</b>: " + selectedUpgrades[0].description;
        }
        if (selectedUpgrades[1].upgradeType == UpgradeType.WeaponUpgrade)
        {
            upgrade2.text = "<b>"+ selectedUpgrades[1].upgradeName+"</b>: " + Player.Instance.weaponManager.GetLevelUpDescription(selectedUpgrades[1].weaponType);
        }
        else
        {
            upgrade2.text = "<b>"+ selectedUpgrades[1].upgradeName+"</b>: " + selectedUpgrades[1].description;
        }
        if (selectedUpgrades[2].upgradeType == UpgradeType.WeaponUpgrade)
        {
            upgrade3.text = "<b>"+ selectedUpgrades[2].upgradeName+"</b>: " + Player.Instance.weaponManager.GetLevelUpDescription(selectedUpgrades[2].weaponType);
        }
        else
        {
            upgrade3.text = "<b>"+ selectedUpgrades[2].upgradeName+"</b>: " + selectedUpgrades[2].description;
        }

        upgrade1Icon.sprite = selectedUpgrades[0].icon;
        upgrade2Icon.sprite = selectedUpgrades[1].icon;
        upgrade3Icon.sprite = selectedUpgrades[2].icon;

        levelUpScreen.SetActive(true);
    }

    public void HideLevelUpScreen()
    {
        levelUpScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpgradeSelected()
    {
        HideLevelUpScreen();
    }
}
