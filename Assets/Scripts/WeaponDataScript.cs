using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponDataScript : ScriptableObject
{
    public string weaponName;
    public GameObject projectilePrefab;
}

[CreateAssetMenu(fileName = "SpikeBallData", menuName = "Weapons/Spike Ball")]
public class SpikeBallData : WeaponDataScript
{
    public List<SpikeBallLevel> levels;
}

[CreateAssetMenu(fileName = "SlingShotData", menuName = "Weapons/Slingshot")]
public class SlingshotData : WeaponDataScript
{
    public List<SlingshotLevel> levels;
}