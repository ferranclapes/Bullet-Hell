using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public PlayerStatsScript stats;
    public PlayerMovementScript movement;
    public WeaponManager weaponManager;

    void Awake()
    {
        Instance = this;

        stats = GetComponent<PlayerStatsScript>();
        movement = GetComponent<PlayerMovementScript>();
        weaponManager = GetComponent<WeaponManager>();
    }
}
