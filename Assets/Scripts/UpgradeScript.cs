using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    BulletDamage,
    BulletSpeed,
    ShootingSpeed,
    PlayerSpeed,
    CureHealth,
    UpgradeHealth,
    Magnet,
    UpgradeMagnet
}



[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade/Upgrade")]
public class UpgradeScript : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;
    public float weight;

    public UpgradeType upgradeType;
    public float upgradePercentage;
}
