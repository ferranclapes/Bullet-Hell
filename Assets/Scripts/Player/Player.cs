using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public PlayerStats stats;
    public PlayerMovement movement;
    public WeaponManager weaponManager;

    void Awake()
    {
        Instance = this;

        stats = GetComponent<PlayerStats>();
        movement = GetComponent<PlayerMovement>();
        weaponManager = GetComponent<WeaponManager>();
    }
}
