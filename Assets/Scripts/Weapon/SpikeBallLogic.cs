using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallLogicScript : WeaponLogic
{
    [SerializeField] private SpikeBallData data;

    private float spawnTimer = 0f;

    void Start()
    {
        weaponType = WeaponType.SpikeBall;
        spawnTimer = data.levels[currentLevel].spawnCooldown; // Start with an immediate spawn
    }
    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= data.levels[currentLevel].spawnCooldown * Player.Instance.stats.cooldownPercentage / 100f)
        {
            SpawnSpikeBall();
            spawnTimer = -data.levels[currentLevel].lifetime; //This way spawnInterval starts from the moment the spike balls dispawn
        }
    }

    private void SpawnSpikeBall()
    {

        Vector3 spawnPosition = new Vector3(transform.position.x + data.levels[currentLevel].spawnRadius, transform.position.y + data.levels[currentLevel].spawnRadius, 0);
        for (int i=0; i < data.levels[currentLevel].projectileCount; i++)
        {
            SpikeBallProjectile spikeBall = Instantiate(data.projectilePrefab, spawnPosition, Quaternion.identity).GetComponent<SpikeBallProjectile>();
            spikeBall.transform.parent = transform;
            spikeBall.Initiate(data.levels[currentLevel].damage, data.levels[currentLevel].speed, data.levels[currentLevel].lifetime, data.levels[currentLevel].spawnRadius, i, data.levels[currentLevel].projectileCount);
        }
    }

    public override string GetWeaponLevelUpDescription()
    {
        if (!this.enabled) return data.levels[currentLevel].levelDescription;
        else return data.levels[currentLevel+1].levelDescription;
    }
}
