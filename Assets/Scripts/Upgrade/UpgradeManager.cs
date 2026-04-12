using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<Upgrade> allUpgrades;
    private List<Upgrade> selectedUpgrades = new List<Upgrade>();

    [SerializeField] private GameObject player;
    private PlayerMovement playerMovementScript;
    private PlayerStats playerStatsScript;
    private WeaponManager weaponManager;

    // Start is called before the first frame update
    
    void Start()
    {
        playerMovementScript = Player.Instance.movement;
        playerStatsScript = Player.Instance.stats;
        weaponManager = Player.Instance.weaponManager;
    }

    public List<Upgrade> Get3Upgrades()
    {
        selectedUpgrades.Clear();
        List<Upgrade> pool = new List<Upgrade>(allUpgrades);

        for (int i = 0; i < 3; i++)
        {
            if(pool.Count == 0) break;

            float totalWeight = 0f;
            foreach (Upgrade upgrade in pool) totalWeight += upgrade.weight;
            float randomWeight = Random.Range(0, totalWeight);
            float cumulativeWeight = 0f;
            int index = 0;
            foreach (Upgrade upgrade in pool)
            {
                cumulativeWeight += upgrade.weight;
                if (randomWeight <= cumulativeWeight) break;
                index++;
            }
            if (index >= pool.Count)
            {
                Debug.LogError("Upgrade selection error: index out of range. Check weights.");
                Debug.Log("Total Weight: " + totalWeight + ", Random Weight: " + randomWeight);
                Debug.Log("Index: " + index + ", Pool Count: " + pool.Count);
                index = pool.Count - 1; // Fallback to last item
            }
            
            if (!playerStatsScript.CanAddUpgrade(pool[index]))
            {
                i--;
                pool.RemoveAt(index);
                continue;
            }
            selectedUpgrades.Add(pool[index]);
            pool.RemoveAt(index);
        }
        return selectedUpgrades;
    }

    public void UpgradeXSelected(int index)
    {
        Upgrade selectedUpgrade = selectedUpgrades[index];

        switch (selectedUpgrade.upgradeType)
        {
            case UpgradeType.PlayerSpeed:
                playerMovementScript.IncreasePlayerSpeed(selectedUpgrade.upgradePercentage);
                break;
            case UpgradeType.CureHealth:
                playerStatsScript.Heal(selectedUpgrade.upgradePercentage);
                break;
            case UpgradeType.UpgradeHealth:
                playerStatsScript.IncreaseMaxHealth(selectedUpgrade.upgradePercentage);
                break;
            case UpgradeType.Magnet:
                GotMagnet(selectedUpgrade);
                break;
            case UpgradeType.UpgradeMagnet:
                playerStatsScript.IncreaseMagnetRange(selectedUpgrade.upgradePercentage);
                break;
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(selectedUpgrade.weaponType);
                break;

        }
    }

    private void GotMagnet(Upgrade magnetUpgrade)
    {
        playerStatsScript.ActivateMagnet(magnetUpgrade.upgradePercentage);

        magnetUpgrade.weight = 0f;

        foreach (Upgrade upgrade in allUpgrades)
        {
            if (upgrade.upgradeType == UpgradeType.UpgradeMagnet) upgrade.weight = 1f;
        }
    }
    
}
