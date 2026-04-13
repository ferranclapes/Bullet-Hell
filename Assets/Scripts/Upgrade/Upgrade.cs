using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    Damage,
    ProjectileSpeed,
    Cooldown,
    Area,
    Duration,
    PlayerSpeed,
    RecoverHealth,
    Armor,
    Experience,
    UpgradeHealth,
    Magnet,
    WeaponUpgrade
}



[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/General Upgrade")]
public class Upgrade : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;
    public float weight;

    public UpgradeType upgradeType;
    public float upgradePercentage;
    public WeaponType weaponType;
    public int maxLevel;
}
