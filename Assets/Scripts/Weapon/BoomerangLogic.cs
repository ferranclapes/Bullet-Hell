using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangLogic : WeaponLogic
{
    [SerializeField] private BoomerangData data;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        weaponType = WeaponType.Boomerang;
        timer = data.levels[currentLevel].shootCooldown; // Start with an immediate shot
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= data.levels[currentLevel].shootCooldown)
        {
            StartCoroutine(Shoot(data.levels[currentLevel].projectileCount));
            timer = 0;
        }
    }

    IEnumerator Shoot(int projectilesLeft)
    {
        BoomerangProjectile projectile = Instantiate(data.projectilePrefab, transform.position, Quaternion.identity).GetComponent<BoomerangProjectile>();
        projectile.transform.parent = transform;
        projectile.Initiate(data.levels[currentLevel].damage, data.levels[currentLevel].speed);
        yield return new WaitForSeconds(data.levels[currentLevel].projectileInterval);
        if (data.levels[currentLevel].projectileCount > 1)
        {
            StartCoroutine(Shoot(projectilesLeft - 1));
        }
    }

    public override string GetWeaponLevelUpDescription()
    {
        return data.levels[currentLevel+1].levelDescription;
    }
}
