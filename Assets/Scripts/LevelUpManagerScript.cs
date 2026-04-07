using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject levelUpScreen;
    [SerializeField] private TextMeshProUGUI fromToLevelText;

    [SerializeField] private TextMeshProUGUI upgrade1;
    [SerializeField] private TextMeshProUGUI upgrade2;
    [SerializeField] private TextMeshProUGUI upgrade3;

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
        upgrade1.text = selectedUpgrades[0].upgradeName;
        upgrade2.text = selectedUpgrades[1].upgradeName;
        upgrade3.text = selectedUpgrades[2].upgradeName;

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
