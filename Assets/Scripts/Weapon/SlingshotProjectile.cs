using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotProjectile : MonoBehaviour
{
    private float speed;
    private float damage;
    private int piercingCount;

    private float lifeTime = 5f;

    private Rigidbody2D rb;

    public void Initiate(float dm, float sp, int pier)
    {
        damage = dm * Player.Instance.stats.damagePercentage / 100f;
        speed = sp * Player.Instance.stats.speedPercentage / 100f;
        piercingCount = pier;
        transform.localScale = transform.localScale * Player.Instance.stats.areaPercentage / 100f;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyLogic enemy = other.GetComponent<EnemyLogic>();
            if (enemy != null)
            {
                piercingCount--;
                enemy.TakeDamage(damage);
                if (piercingCount < 0) Destroy(gameObject);
            }
            else Destroy(gameObject);
        }
    }
}
