using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponData : ScriptableObject
{
    public string weaponName;
    public GameObject projectilePrefab;
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
    public List<BoomerangLevel> levels;
}