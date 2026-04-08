using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManagerScript : MonoBehaviour
{
    [SerializeField] private List<UpgradeScript> allUpgrades;
    private List<UpgradeScript> selectedUpgrades = new List<UpgradeScript>();

    [SerializeField] private GameObject player;
    private ShooterScript shooterScript;
    private PlayerMovementScript playerMovementScript;
    private PlayerStatsScript playerStatsScript;

    // Start is called before the first frame update
    
    void Start()
    {
        shooterScript = player.GetComponent<ShooterScript>();
        playerMovementScript = player.GetComponent<PlayerMovementScript>();
        playerStatsScript = player.GetComponent<PlayerStatsScript>();
    }

    public List<UpgradeScript> Get3Upgrades()
    {
        selectedUpgrades.Clear();
        List<UpgradeScript> pool = new List<UpgradeScript>(allUpgrades);

        for (int i = 0; i < 3; i++)
        {
            if(pool.Count == 0) break;

            float totalWeight = 0f;
            foreach (UpgradeScript upgrade in pool) totalWeight += upgrade.weight;
            float randomWeight = Random.Range(0, totalWeight);
            float cumulativeWeight = 0f;
            int index = 0;
            foreach (UpgradeScript upgrade in pool)
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
            
            selectedUpgrades.Add(pool[index]);
            pool.RemoveAt(index);
        }
        return selectedUpgrades;
    }

    public void UpgradeXSelected(int index)
    {
        UpgradeScript selectedUpgrade = selectedUpgrades[index];

        switch (selectedUpgrade.upgradeType)
        {
            case UpgradeType.BulletDamage:
                shooterScript.IncreaseBulletDamage(selectedUpgrade.upgradePercentage);
                break;
            case UpgradeType.BulletSpeed:
                shooterScript.IncreaseBulletSpeed(selectedUpgrade.upgradePercentage);
                break;
            case UpgradeType.ShootingSpeed:
                shooterScript.IncreaseShootingSpeed(selectedUpgrade.upgradePercentage);
                break;
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
                playerStatsScript.ActivateMagnet(selectedUpgrade.upgradePercentage);
                break;
            case UpgradeType.UpgradeMagnet:
                playerStatsScript.IncreaseMagnetRange(selectedUpgrade.upgradePercentage);
                break;
        }
    }
}
