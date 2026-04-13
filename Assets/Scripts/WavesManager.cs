using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private List<Waves> waves;
    private int currentWave;
    private int maxEnemies = 300;
    private float waveTimer;
    private float spawnTimer;
    private bool isSpawning = false;
    private bool isBossSpawned = false;

    void Start()
    {
        currentWave = 1;
        waveTimer = 0f;
        spawnTimer = 0f;

    }

    void Update()
    {
        waveTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;
        if (waveTimer >= currentWave * 60f)
        {
            StopCoroutine(SpawnEnemies());
            currentWave++;
            isBossSpawned = false;
        }
        if (EnemyManager.enemies.Count <= waves[currentWave-1].minEnemies && spawnTimer >= waves[currentWave - 1].spawnRate && !isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
        if(waves[currentWave-1].bosses.Count > 0 && !isBossSpawned)
        {
            isBossSpawned = true;
            StartCoroutine(SpawnBosses());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        if (EnemyManager.enemies.Count >= maxEnemies) yield return null;
        foreach (EnemyData enemy in waves[currentWave-1].enemies)
        {
            EnemyLogic enemyInstance = Instantiate(enemy.prefab, GetSpawnPosition(), Quaternion.identity).GetComponent<EnemyLogic>();
            enemyInstance.Initialize(enemy);
            yield return new WaitForSeconds(waves[currentWave-1].spawnRate);
        }
        spawnTimer = 0f;
        isSpawning = false;
    }

    private IEnumerator SpawnBosses()
    {
        foreach (EnemyData boss in waves[currentWave - 1].bosses)
        {
            EnemyLogic enemyInstance = Instantiate(boss.prefab, GetSpawnPosition(), Quaternion.identity).GetComponent<EnemyLogic>();
            enemyInstance.Initialize(boss);
            yield return new WaitForSeconds(waves[currentWave - 1].spawnRate);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Camera cam = Camera.main;
        float x = 0;
        float y = 0;
        
        // 1. Randomly pick which side of the screen to spawn on
        int side = Random.Range(0, 4); 

        // Padding ensures they spawn a bit further out (e.g., 0.1 units)
        float padding = 0.1f; 

        switch (side)
        {
            case 0: // Top
                x = Random.Range(-padding, 1 + padding);
                y = 1 + padding;
                break;
            case 1: // Bottom
                x = Random.Range(-padding, 1 + padding);
                y = -padding;
                break;
            case 2: // Left
                x = -padding;
                y = Random.Range(-padding, 1 + padding);
                break;
            case 3: // Right
                x = 1 + padding;
                y = Random.Range(-padding, 1 + padding);
                break;
        }

        // 2. Convert the Viewport point (0-1) to a World Point (X, Y in your game)
        Vector3 viewportPoint = new Vector3(x, y, cam.nearClipPlane);
        Vector3 worldPoint = cam.ViewportToWorldPoint(viewportPoint);

        // 3. Ensure the Z is 0 so enemies don't spawn "inside" the camera
        worldPoint.z = 0;

        return worldPoint;
    }
}
