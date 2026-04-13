using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponData : ScriptableObject
{
    public string weaponName;
    public GameObject projectilePrefab;
    public float knockback;
}

[CreateAssetMenu(fileName = "SpikeBallData", menuName = "Weapons/Spike Ball")]
public class SpikeBallData : WeaponData
{
    public List<SpikeBallLevel> levels;
}

[CreateAssetMenu(fileName = "SlingShotData", menuName = "Weapons/Slingshot")]
public class SlingshotData : WeaponData
{
    public List<SlingshotLevel> levels;
}

[CreateAssetMenu(fileName = "BoomerangData", menuName = "Weapons/Boomerang")]
public class BoomerangData : WeaponData
{
    public float returnTime;
    public List<BoomerangLevel> levels;
}

[CreateAssetMenu(fileName = "AuraData", menuName = "Weapons/Aura")]
public class AuraData : WeaponData
{
    public List<AuraLevel> levels;
}