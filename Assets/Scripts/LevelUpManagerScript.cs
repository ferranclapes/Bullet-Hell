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
    
    void Start()
    {
        levelUpScreen.SetActive(false);
    }
    public void ShowLevelUpScreen(int newLevel)
    {
        Time.timeScale = 0f;
        fromToLevelText.text =  (newLevel-1).ToString() + " -> " + newLevel.ToString();

        upgrade1.text = "Upgrade 1";
        upgrade2.text = "Upgrade 2";
        upgrade3.text = "Upgrade 3";

        levelUpScreen.SetActive(true);
    }

    public void HideLevelUpScreen()
    {
        levelUpScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Upgrade1Selected()
    {
        Debug.Log("Upgrade 1 Selected");
        HideLevelUpScreen();
    }

    public void Upgrade2Selected()
    {
        Debug.Log("Upgrade 2 Selected");
        HideLevelUpScreen();
    }

    public void Upgrade3Selected()
    {
        Debug.Log("Upgrade 3 Selected");
        HideLevelUpScreen();
    }
}
