using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyData> enemyPool;
    [SerializeField] private float spawnInterval = 2f;
    [Header("Spawn Area")]
    [SerializeField] private float xRange = 3f;
    [SerializeField] private float yRange = 3f;

    [SerializeField] private Transform enemiesParent;
    [SerializeField] private Transform pickableParent;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        float xOffset = Random.Range(-xRange/2, xRange/2);
        float yOffset = Random.Range(-yRange/2, yRange/2);

        Vector3 spawnPosition = transform.position + new Vector3(xOffset, yOffset, 0f);

        
        EnemyData selectedEnemy = GetWeightedRandomEnemy();
        if (selectedEnemy == null)
        {
            Debug.LogWarning("Enemy pool is empty or all weights are zero.");
            return;
        }
        GameObject enemy = Instantiate(selectedEnemy.prefab, spawnPosition, Quaternion.identity);
        if (enemy.TryGetComponent(out EnemyLogic enemyScript))
        {
            enemyScript.Initialize(selectedEnemy);
        }

        enemy.transform.SetParent(enemiesParent);
        enemy.GetComponent<EnemyLogic>().setPickableParent(pickableParent);
    }

    private EnemyData GetWeightedRandomEnemy()
    {
        int totalWeight = 0;
        foreach(EnemyData enemy in enemyPool)
        {
            totalWeight += enemy.spawnWeight;
        }

        if (totalWeight == 0) return null;

        int randomWeight = Random.Range(0, totalWeight);
        foreach(EnemyData enemy in enemyPool)
        {
            if (randomWeight < enemy.spawnWeight)
            {
                return enemy;
            }
            randomWeight -= enemy.spawnWeight;
        }
        return null;
    }
}
