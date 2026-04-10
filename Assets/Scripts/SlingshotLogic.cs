using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotLogic : WeaponLogic
{
    [SerializeField] private SlingshotData data;
    private float timer = 0f;

    void Start()
    {
        weaponType = WeaponType.Slingshot;
        timer = data.levels[currentLevel].shootCooldown; // Start with an immediate shot
    }
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 direction = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        BulletScript bulletScript = Instantiate(data.projectilePrefab, transform.position, rotation).GetComponent<BulletScript>();
        bulletScript.transform.parent = transform;
        bulletScript.Initiate(data.levels[currentLevel].damage, data.levels[currentLevel].speed, data.levels[currentLevel].piercingCount);
        yield return new WaitForSeconds(data.levels[currentLevel].projectileInverval);
        if (projectilesLeft > 1)
        {
            StartCoroutine(Shoot(projectilesLeft - 1));
        }
    }

    public override string GetWeaponLevelUpDescription()
    {
        return data.levels[currentLevel+1].levelDescription;
    }
}
