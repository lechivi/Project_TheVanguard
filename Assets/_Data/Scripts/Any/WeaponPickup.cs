using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private RaycastWeapon raycastWeaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        PlayerWeaponActiveOld activeWeapon = other.GetComponentInChildren<PlayerWeaponActiveOld>();
        if (activeWeapon)
        {
            RaycastWeapon weapon = Instantiate(raycastWeaponPrefab);
            activeWeapon.Equip(weapon);
        }
    }
}
