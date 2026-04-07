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

    // Start is called before the first frame update
    
    void Start()
    {
        shooterScript = player.GetComponent<ShooterScript>();
        playerMovementScript = player.GetComponent<PlayerMovementScript>();
    }
    public List<UpgradeScript> Get3Upgrades()
    {
        selectedUpgrades.Clear();
        List<UpgradeScript> pool = new List<UpgradeScript>(allUpgrades);

        for (int i = 0; i < 3; i++)
        {
            if(pool.Count == 0) break;

            int index = Random.Range(0, pool.Count);
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
        }
    }
}
