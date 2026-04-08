using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    private float health;
    private float maxHealth = 100f;
    private int xp = 0;
    private int currentLevel = 1;
    private int xpToNextLevel = 5;
    private int xpLastLevelUp = 0;
    private bool haveMagnet = false;
    private float magnetRange = 1f;
    private CircleCollider2D magnetCollider;
    [SerializeField] private UIManagerScript uiManager;
    [SerializeField] private LevelUpManagerScript levelUpManager;

    void Start()
    {
        magnetCollider = GetComponent<CircleCollider2D>();
        magnetCollider.enabled = false;;

        uiManager.UpdateHealth(health);
        uiManager.UpdateXP(xp, xpToNextLevel);
        uiManager.UpdateLevel(currentLevel);

        health = maxHealth;
        uiManager.UpdateHealth(health);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            xp = xpToNextLevel;
            LevelUp();
        }
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
        xpToNextLevel += 10*(currentLevel-1) + 5;
        uiManager.UpdateLevel(currentLevel);
        uiManager.UpdateXP(xp - xpLastLevelUp, xpToNextLevel);
        levelUpManager.ShowLevelUpScreen(currentLevel);
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth) health = maxHealth;
        uiManager.UpdateHealth(health);
    }

    public void IncreaseMaxHealth(float percentage)
    {
        int tempMaxHealth = (int)maxHealth;
        maxHealth += maxHealth * (percentage/100);
        health = health*(maxHealth / tempMaxHealth);
        uiManager.UpdateHealth(health);
    }

    public void ActivateMagnet(float range)
    {
        haveMagnet = true;
        magnetRange = range;
        magnetCollider.radius = magnetRange;
        magnetCollider.enabled = true;
    }

    public void IncreaseMagnetRange(float percentage)
    {
        if (!haveMagnet) return;

        magnetRange += magnetRange * (percentage/100);
        magnetCollider.radius = magnetRange;
    }

}
