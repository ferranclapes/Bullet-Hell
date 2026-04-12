using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : MonoBehaviour
{
    private float damage;
    private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private float returnTime;
    private float accelerationTime;
    
    private Rigidbody2D rb;
    private Vector3 direction;
    private float timer = 0f;

    public void Initiate(float dm, float sp, float rt)
    {
        damage = dm * Player.Instance.stats.damagePercentage / 100f;
        speed = sp * Player.Instance.stats.speedPercentage / 100f;
        returnTime = rt;
        accelerationTime = returnTime * 2f;
        transform.localScale = transform.localScale * Player.Instance.stats.areaPercentage / 100f;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Transform nearestEnemy = FindNearestEnemy();
        direction = nearestEnemy != null ? (nearestEnemy.position - transform.position).normalized : transform.right;

        rb.velocity = direction * speed;
        rb.angularVelocity = 360f * 3;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= returnTime && timer >= returnTime/2) rb.velocity = Vector2.Lerp(direction * speed, Vector2.zero, (timer - returnTime/2) / (returnTime / 2));
        else if (timer >= returnTime) rb.velocity = Vector2.Lerp(Vector2.zero, direction * -speed, (timer - returnTime) / (accelerationTime - returnTime));
        

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyLogic enemy = collision.GetComponent<EnemyLogic>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    private Transform FindNearestEnemy()
    {
        Transform nearestEnemy = null;
        float minDist = Mathf.Infinity;

        foreach (EnemyLogic enemy in EnemyManager.enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearestEnemy = enemy.transform;
            }
        }
        return nearestEnemy;
    }
}
