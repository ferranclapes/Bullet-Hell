using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waves
{
    public int waveNumber;
    public float spawnRate;
    [Space(10)]
    public int minEnemies;
    public List<EnemyData> enemies;
    public List<EnemyData> bosses;
}


