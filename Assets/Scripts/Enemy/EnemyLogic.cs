using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private EnemyData enemyData;
    private float currentHealth;
    private float currentSpeed;
    private bool isKnockedback = false;
    private bool inAura = false;

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
        if (!isKnockedback)
        {
            Vector2 direction = (playerTransform.position - transform.position);
            if (direction.magnitude > 22f)
            {
                EnemyManager.enemies.Remove(this);
                Destroy(gameObject);
                return;
            }
            direction.Normalize();
            rb.velocity = direction * currentSpeed;
        }
    }

    public void TakeDamage(float damage,float knockback)
    {
        currentHealth -= damage;
        DamageText damageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity).GetComponent<DamageText>();
        damageText.SetDamageText(damage);
        Vector2 knockbackDirection = (transform.position - playerTransform.position).normalized;
        rb.velocity = Vector2.zero;
        rb.AddForce(knockbackDirection * knockback, ForceMode2D.Impulse);
        if(knockback != 0)
        {
            isKnockedback = true;
            StartCoroutine(ResetKnockback());
        }
    }

    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(0.2f);
        isKnockedback = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().TakeDamage(enemyData.damage);
            EnemyManager.enemies.Remove(this);
            Destroy(gameObject);
        }
        if (other.CompareTag("Aura"))
        {
            inAura = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Aura"))
        {
            inAura = false;
        }
    }

    public void TakeDamageByAura(float damage, float cooldown)
    {
        StartCoroutine(AuraCoroutine(damage, cooldown));
    }
    private IEnumerator AuraCoroutine(float damage, float cooldown)
    {
        while (inAura)
        {
            TakeDamage(damage,0f);
            yield return new WaitForSeconds(cooldown);
        }
    }

    public void setPickableParent(Transform parent)
    {
        pickableParent = parent;
    }
}
