using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickableType
{
    XP,
    Health
}
public class PickableScript : MonoBehaviour
{
    [SerializeField] private PickableType type;
    [SerializeField] private float magnetStrength = 0.01f;
    private bool inRange = false;
    private Transform player;
    private PlayerStatsScript playerStats;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inRange)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, magnetStrength);
            if (Vector3.Distance(transform.position, player.position) < 0.1f)
            {
                playerStats.PickUp(type);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
            player = other.gameObject.transform;
            playerStats = other.gameObject.GetComponent<PlayerStatsScript>();
        }
    }
}
