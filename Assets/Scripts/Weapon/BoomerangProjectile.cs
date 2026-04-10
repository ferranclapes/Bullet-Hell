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

    public void Initiate(float dm, float sp)
    {
        damage = dm;
        speed = sp;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Transform nearestEnemy = FindNearestEnemy();
        direction = nearestEnemy != null ? (nearestEnemy.position - transform.position).normalized : transform.right;

        accelerationTime = returnTime * 2f;

        rb.velocity = direction * speed;
        rb.angularVelocity = 360f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= returnTime) rb.velocity = Vector2.Lerp(direction * speed, Vector2.zero, timer / returnTime);
        else rb.velocity = Vector2.Lerp(Vector2.zero, direction * -speed, (timer - returnTime) / (accelerationTime - returnTime));
        

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
