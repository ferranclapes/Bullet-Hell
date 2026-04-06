using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float health = 10f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private GameObject dropPrefab;
    private Transform pickableParent;
    private Transform playerTransform;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (dropPrefab != null)
            {
                GameObject drop =Instantiate(dropPrefab, transform.position, Quaternion.identity);
                drop.transform.parent = pickableParent;
            }
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatsScript>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void setPickableParent(Transform parent)
    {
        pickableParent = parent;
    }
}
