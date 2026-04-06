using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    private float health = 100f;
    private int xp = 0;
    private int currentLevel = 1;
    private int xpToNextLevel = 5;
    private int xpLastLevelUp = 0;
    [SerializeField] private UIManagerScript uiManager;
    [SerializeField] private LevelUpManagerScript levelUpManager;

    void Start()
    {
        uiManager.UpdateHealth(health);
        uiManager.UpdateXP(xp, xpToNextLevel);
        uiManager.UpdateLevel(currentLevel);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        uiManager.UpdateHealth(health);
        //Todo: Implement Game Over
    }

    public void PickUp(PickableType type)
    {
        switch (type)
        {
            case PickableType.XP:
                xp++;
                uiManager.UpdateXP(xp - xpLastLevelUp, xpToNextLevel);
                if (xp >= xpToNextLevel)
                {
                    LevelUp();
                }
                break;
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        xpLastLevelUp = xpToNextLevel;
        xpToNextLevel += 10*(currentLevel-1) + 5; // Increase XP needed for next level
        uiManager.UpdateLevel(currentLevel);
        uiManager.UpdateXP(xp - xpLastLevelUp, xpToNextLevel);
        levelUpManager.ShowLevelUpScreen(currentLevel);
    }
}
