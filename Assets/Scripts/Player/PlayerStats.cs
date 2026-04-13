using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float health;
    private float maxHealth = 100f;
    private float maxHealthPercentage = 100f;
    private float recoveryRate = 0f;
    private float armour = 0f;
    
    private float xp = 0;
    private int currentLevel = 1;
    private int xpToNextLevel = 5;
    private int xpLastLevelUp = 0;
    private float xpPercentage = 100f;
    
    private bool haveMagnet = false;
    private float magnetRange = 1f;
    private CircleCollider2D magnetCollider;

    [Header("Weapons Stats")]
    public float damagePercentage = 100f;
    public float projectileSpeedPercentage = 100f;
    public float cooldownPercentage = 100f;
    public float areaPercentage = 100f;
    public float durationPercentage = 100f;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LevelUpManager levelUpManager;

    private Dictionary<string, int> activeUpgrades = new Dictionary<string, int>();

    void Start()
    {
        magnetCollider = GetComponent<CircleCollider2D>();
        magnetCollider.enabled = false;;

        uiManager.UpdateHealth(health);
        uiManager.UpdateXP(xp, xpToNextLevel);
        uiManager.UpdateLevel(currentLevel);

        health = maxHealth;
        uiManager.UpdateHealth(health);
        if (recoveryRate != 0)
        {
            StartCoroutine(RecoverHealth());
        }
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
        damage = damage - armour;
        if (damage < 0) damage = 0;
        health -= damage;
        uiManager.UpdateHealth(health);
        //Todo: Implement Game Over
    }

    public void PickUp(PickableType type, float value)
    {
        switch (type)
        {
            case PickableType.XP:
                xp += value * (xpPercentage / 100f);
                uiManager.UpdateXP(xp - xpLastLevelUp, xpToNextLevel);
                if (xp >= xpToNextLevel)
                {
                    LevelUp();
                }
                break;
            case PickableType.Health:
                Heal(value);
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

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void IncreaseXPPercentage(float percentage)
    {
        xpPercentage += percentage;
    }
    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth * maxHealthPercentage / 100f) health = maxHealth * maxHealthPercentage / 100f;
        uiManager.UpdateHealth(health);
    }
    private IEnumerator RecoverHealth()
    {
        while (recoveryRate != 0)
        {
            yield return new WaitForSeconds(1f);
            Heal(recoveryRate);
        }
    }

    public void IncreaseMaxHealth(float percentage)
    {
        float tempHealthpercentage = health / (maxHealth * maxHealthPercentage / 100f);
        maxHealthPercentage = maxHealthPercentage + percentage;
        health = tempHealthpercentage * (maxHealth * maxHealthPercentage / 100f);
        uiManager.UpdateHealth(health);
    }

    public void IncreaseRecoveryRate(float amount)
    {
        recoveryRate += amount;
        if (recoveryRate > 0 && !IsInvoking(nameof(RecoverHealth)))
        {
            StartCoroutine(RecoverHealth());
        }
    }

    public void IncreaseArmour(float amount)
    {
        armour += amount;
    }

    public bool ActivateMagnet(float range)
    {
        if (haveMagnet) return false;

        haveMagnet = true;
        magnetRange = range;
        magnetCollider.radius = magnetRange;
        magnetCollider.enabled = true;
        return true;
    }

    public void IncreaseMagnetRange(float percentage)
    {
        if (!haveMagnet) return;

        magnetRange += magnetRange * (percentage/100);
        magnetCollider.radius = magnetRange;
    }

    public bool AddUpgrade(Upgrade upgrade)
    {
        if (upgrade.upgradeType == UpgradeType.WeaponUpgrade)
        {
            if (activeUpgrades.ContainsKey(upgrade.weaponType.ToString()))activeUpgrades[upgrade.weaponType.ToString()]++;
            else activeUpgrades.Add(upgrade.weaponType.ToString(), 1);

            if (activeUpgrades[upgrade.weaponType.ToString()] >= upgrade.maxLevel)
            {
                activeUpgrades[upgrade.upgradeName] = upgrade.maxLevel;
                return false;
            }
            else return true;
        }
        else
        {
            if (activeUpgrades.ContainsKey(upgrade.upgradeName))activeUpgrades[upgrade.upgradeName]++;
            else activeUpgrades.Add(upgrade.upgradeName, 1);

            if (activeUpgrades[upgrade.upgradeName] >= upgrade.maxLevel)
            {
                activeUpgrades[upgrade.upgradeName] = upgrade.maxLevel;
                return false;
            }
            else return true;
        }

    }

    public bool CanAddUpgrade(Upgrade upgrade)
    {
        if (activeUpgrades.ContainsKey(upgrade.upgradeName)) return activeUpgrades[upgrade.upgradeName] < upgrade.maxLevel;
        else return true;
    }

}
