using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private EnemyData enemyData;
    private float currentHealth;
    private float currentSpeed;
    [SerializeField] private GameObject dropPrefab;
    private Transform pickableParent;
    private Transform playerTransform;
    private Rigidbody2D rb;

    [Header("Damage Indicator")]
    [SerializeField] private GameObject damageTextPrefab;
    // Start is called before the first frame update
    
    public void Initialize(EnemyData data)
    {
        enemyData = data;

        playerTransform = Player.Instance.transform;
        if (enemyData.healthToLevel)
        {
            currentHealth = enemyData.maxHealth + (enemyData.maxHealth * (playerTransform.GetComponent<PlayerStats>().GetCurrentLevel() - 1));
        }
        else
        {
            currentHealth = enemyData.maxHealth;
        }

        currentSpeed = enemyData.moveSpeed;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnemyManager.enemies.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            if (dropPrefab != null)
            {
                GameObject drop =Instantiate(dropPrefab, transform.position, Quaternion.identity);
                drop.GetComponent<Pickable>().SetValue(enemyData.xpToDrop);
                drop.transform.parent = pickableParent;
            }
            EnemyManager.enemies.Remove(this);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = (playerTransform.position - transform.position);
        if (direction.magnitude > 15f)
        {
            EnemyManager.enemies.Remove(this);
            Destroy(gameObject);
            return;
        }
        direction.Normalize();
        rb.velocity = direction * currentSpeed;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        DamageText damageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity).GetComponent<DamageText>();
        damageText.SetDamageText(damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().TakeDamage(enemyData.damage);
            EnemyManager.enemies.Remove(this);
            Destroy(gameObject);
        }
    }

    public void setPickableParent(Transform parent)
    {
        pickableParent = parent;
    }
}
