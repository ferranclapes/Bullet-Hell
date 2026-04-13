using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponLogic : MonoBehaviour
{
    public WeaponType weaponType;
    public int currentLevel = 0;
    public abstract string GetWeaponLevelUpDescription();
    public virtual void UpgradeWeapon()
    {
        currentLevel++;
    }
}
