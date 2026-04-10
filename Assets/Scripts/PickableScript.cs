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
    [SerializeField] private float value;
    [SerializeField] private float magnetStrength = 0.01f;
    private bool inRange = false;
    private Transform player;
    private PlayerStatsScript playerStats;

    void Start()
    {
        player = Player.Instance.transform;
        playerStats = Player.Instance.stats;
    }

    void FixedUpdate()
    {
        if (inRange)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, magnetStrength);
            if (Vector3.Distance(transform.position, player.position) < 0.5f)
            {
                playerStats.PickUp(type, value);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    public void SetValue(float newValue)
    {
        value = newValue;
    }
}
