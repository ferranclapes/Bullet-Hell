using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    Slingshot,
    SpikeBall
}
public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<WeaponLogic> allWeapons;
    private Dictionary<WeaponType, WeaponLogic> weaponDictionary = new Dictionary<WeaponType, WeaponLogic>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (WeaponLogic weapon in allWeapons) weaponDictionary.Add(weapon.weaponType, weapon);
    }

    public string GetLevelUpDescription(WeaponType weaponType)
    {
        if (weaponDictionary.ContainsKey(weaponType)) return weaponDictionary[weaponType].GetWeaponLevelUpDescription();
        else Debug.LogError("Weapon not found");
        return "";
    }

    public void UpgradeWeapon(WeaponType weaponType)
    {
        if (weaponDictionary.ContainsKey(weaponType))
        {
            if(!weaponDictionary[weaponType].enabled) weaponDictionary[weaponType].enabled = true;
            else weaponDictionary[weaponType].UpgradeWeapon();
        }
        else Debug.LogError("Weapon not found");
    }
}
