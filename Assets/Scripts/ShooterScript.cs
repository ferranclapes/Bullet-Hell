using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootCooldown = 0.5f;

    [SerializeField] private Transform bulletsParent;

    [Header("Bullets Stats")]
    [SerializeField] private float bulletDamage = 5f;
    [SerializeField] private float bulletSpeed = 5f;
    private float nextShootIn = 0f;


    // Update is called once per frame
    void Update()
    {
        nextShootIn -= Time.deltaTime;
        if (nextShootIn <= 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            Vector3 direction = (mousePos - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            bullet.transform.SetParent(bulletsParent);
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.SetDamage(bulletDamage);
            bulletScript.SetSpeed(bulletSpeed);
            
            nextShootIn = shootCooldown;
        }
    }

    public void IncreaseBulletDamage(float percentage)
    {
        bulletDamage += bulletDamage * (percentage / 100f);
    }

    public void IncreaseBulletSpeed(float percentage)
    {
        bulletSpeed += bulletSpeed * (percentage / 100f);
    }

    public void IncreaseShootingSpeed(float percentage)
    {
        shootCooldown -= shootCooldown * (percentage / 100f);
    }
}
