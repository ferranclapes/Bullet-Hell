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
