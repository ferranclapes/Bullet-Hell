using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField] private RectTransform health;
    [SerializeField] private RectTransform xp;
    [SerializeField] private TextMeshProUGUI level;
    
    public void UpdateHealth(float newHealth)
    {
        health.sizeDelta = new Vector2(newHealth, health.sizeDelta.y);
    }
    public void UpdateXP(float newXP, float xpToNextLevel)
    {
        xp.sizeDelta = new Vector2(newXP / xpToNextLevel * 100, xp.sizeDelta.y);
    }
    public void UpdateLevel(int newLevel)
    {
        level.text = "Level: " + newLevel.ToString();
    }

}
