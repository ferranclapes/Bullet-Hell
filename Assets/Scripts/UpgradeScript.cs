using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    BulletDamage,
    BulletSpeed,
    ShootingSpeed,
    PlayerSpeed
}


[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade/Upgrade")]
public class UpgradeScript : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;

    public UpgradeType upgradeType;
    public float upgradePercentage;
}
