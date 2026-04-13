using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraLogic : WeaponLogic
{
    [SerializeField] private AuraData data;
    private float areaPercentage = 100f;
    private Vector3 originalScale;

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        weaponType = WeaponType.Aura;
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer.enabled = true;
        circleCollider.enabled = true;
    }
    public override void UpgradeWeapon()
    {
        base.UpgradeWeapon();
        areaPercentage += data.levels[currentLevel].areaUpgradePercentage;
        transform.localScale =  originalScale * (areaPercentage / 100);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyLogic enemy = other.GetComponent<EnemyLogic>();
            enemy.TakeDamageByAura(data.levels[currentLevel].damage, data.levels[currentLevel].cooldown);
        }
    }

    public override string GetWeaponLevelUpDescription()
    {
        if (!this.enabled) return data.levels[currentLevel].levelDescription;
        else return data.levels[currentLevel+1].levelDescription;
    }
}
