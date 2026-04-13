using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NewEnemy", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Identity")]
    public string enemyName;
    public GameObject prefab;

    [Header("Base Stats")]
    public float maxHealth;
    public bool healthToLevel;
    public float moveSpeed;
    public int damage;
    public float xpToDrop;
}
