using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallProjectile : MonoBehaviour
{
    private float damage;
    private float speed;
    public float knockback;
    private float lifetime;
    private float radius;
    private float currentAngle;

    private float maxRadius = 2.5f;
    // Start is called before the first frame update
    
    public void Initiate(float dam, float sp, float lt, float rad, int index, int total, float kb)
    {
        damage = dam * Player.Instance.stats.damagePercentage / 100f;
        speed = sp * Player.Instance.stats.projectileSpeedPercentage / 100f;
        lifetime = lt;
        radius = rad * Player.Instance.stats.areaPercentage / 100f;
        knockback = kb;
        if (radius > maxRadius)
        {
            float difference = radius - maxRadius;
            radius = maxRadius;
            transform.localScale = transform.localScale + new Vector3(difference, difference);
        }

        currentAngle = index * 2f * Mathf.PI / total;
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }

        currentAngle += speed * Time.deltaTime;

        float x = Mathf.Cos(currentAngle) * radius;
        float y = Mathf.Sin(currentAngle) * radius;

        transform.localPosition = new Vector3(x, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyLogic>().TakeDamage(damage, knockback);
        }
    }
}
