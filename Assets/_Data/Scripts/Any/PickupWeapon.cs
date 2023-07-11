using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] private Weapon weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        PlayerWeaponManager weaponManager = other.GetComponentInChildren<PlayerWeaponManager>();
        if (weaponManager)
        {
            Weapon weapon = Instantiate(weaponPrefab);
            weaponManager.AddWeapon(weapon);
            gameObject.SetActive(false);
        }
    }
}
