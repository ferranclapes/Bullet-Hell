using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallHolderScript : MonoBehaviour
{
    [SerializeField] private GameObject spikeBallPrefab;
    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float spawnRadius = 2f;
    [SerializeField] private int spikeBallCount = 1;

    [Header("Spike Ball Settings")]
    [SerializeField] private float spikeBallDamage = 10f;
    [SerializeField] private float spikeBallSpeed = 2f;
    [SerializeField] private float spikeBallLifetime = 3f;

    private float spawnTimer = 0f;

    void Start()
    {
        spawnTimer = spawnInterval; // Start with an immediate spawn
    }
    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnSpikeBall();
            spawnTimer = -spikeBallLifetime; //This way spawnInterval starts from the moment the spike balls dispawn
        }
    }

    private void SpawnSpikeBall()
    {

        Vector3 spawnPosition = new Vector3(transform.position.x + spawnRadius, transform.position.y + spawnRadius, 0);
        for (int i=0; i < spikeBallCount; i++)
        {
            SpikeBallScript spikeBall = Instantiate(spikeBallPrefab, spawnPosition, Quaternion.identity).GetComponent<SpikeBallScript>();
            spikeBall.transform.parent = transform;
            spikeBall.Initiate(spikeBallDamage, spikeBallSpeed, spikeBallLifetime, spawnRadius, i, spikeBallCount);
        }
    }
}
