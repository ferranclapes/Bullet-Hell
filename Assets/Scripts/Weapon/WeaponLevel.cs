using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpikeBallLevel
{
    [TextArea] public string levelDescription;
    public int projectileCount;
    public float damage;
    public float speed;
    public float lifetime;
    public float spawnCooldown;
    public float spawnRadius;
}

[System.Serializable]
public class SlingshotLevel
{
    [TextArea] public string levelDescription;
    public int projectileCount;
    public float damage;
    public float speed;
    public int piercingCount;
    public float shootCooldown;
    public float projectileInverval;
}

[System.Serializable]
public class BoomerangLevel
{
    [TextArea] public string levelDescription;
    public float damage;
    public float speed;
    public float area;
    public int projectileCount;
    public float projectileInterval;
    public float shootCooldown;
}

[System.Serializable]
public class AuraLevel
{
    [TextArea] public string levelDescription;
    public float damage;
    public float areaUpgradePercentage;
    public float cooldown;
}