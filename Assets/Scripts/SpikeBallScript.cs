using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallScript : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float lifetime = 5f;
    private float radius;
    private float currentAngle;
    // Start is called before the first frame update
    
    public void Initiate(float dam, float sp, float lt, float rad, int index, int total)
    {
        damage = dam;
        speed = sp;
        lifetime = lt;
        radius = rad;

        currentAngle = (index / (float)total) * 2f * Mathf.PI;
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
            collision.GetComponent<EnemyScript>().TakeDamage(damage);
        }
    }
}
