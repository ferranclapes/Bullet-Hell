using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [Header("Spawn Area")]
    [SerializeField] private float xRange = 3f;
    [SerializeField] private float yRange = 3f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
