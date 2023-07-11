using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private int maxSlotWeapon = 3;
    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private Transform[] weaponHolderSlots = new Transform[4];

    [Header("Offset Weapon Holder Slot")]
    [SerializeField] private Vector3 offsetPosMeleeHolderBack = new Vector3(0, 0.11f, -0.27f);
    [SerializeField] private Vector3 offsetRotMeleeHolderBack = new Vector3(90, 0, 0);
    [SerializeField] private Vector3 offsetPosMeleeHolderLeft = new Vector3(0, 0, 0.08f);
    [SerializeField] private Vector3 offsetRotMeleeHolderLeft = new Vector3(-90, -90, -90);

    private int currentWeaponIndex = -1;
    private bool isReadySwap = true;
    [SerializeField] private float swapCooldown = 0.2f;

    private void Update()
    {
        if (this.isReadySwap)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                this.SetActiveWeapon(this.currentWeaponIndex + 1, false);
            }

            if (Input.mouseScrollDelta.y > 0)
            {
                this.SetActiveWeapon(this.currentWeaponIndex - 1, false);
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                this.SetActiveWeapon(this.currentWeaponIndex + 1, false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                this.SetActiveWeapon(0, true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                this.SetActiveWeapon(1, true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                this.SetActiveWeapon(2, true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                this.SetActiveWeapon(3, true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                this.SetActiveWeapon(4, true);
            }
        }

    }
    public void AddWeapon(Weapon weapon)
    {
        if (this.weapons.Count >= this.maxSlotWeapon)
        {
            Debug.Log("Max Amount Weapon");
            return;
        }
        this.weapons.Add(weapon);
        Debug.Log(this.weaponHolderSlots[(int)weapon.WeaponSlot[0]]);

        this.SetHolsterForWeapon(weapon);

        if (this.currentWeaponIndex == -1)
        {
            this.SetActiveWeapon(this.weapons.IndexOf(weapon), false);
        }
        else
        {
            weapon.gameObject.SetActive(false);
        }
    }

    private void SetHolsterForWeapon(Weapon weapon)
    {
        weapon.transform.SetParent(this.weaponHolderSlots[(int)weapon.WeaponSlot[0]]); //TODO: Change to RigLayer

        if (weapon.WeaponData.WeaponType == WeaponType.Melee)
        {
            if (weapon.WeaponSlot[0] == WeaponSlot.Back)
            {
                weapon.transform.localPosition = this.offsetPosMeleeHolderBack;
                weapon.transform.localRotation = Quaternion.Euler(this.offsetRotMeleeHolderBack);
                weapon.transform.localScale = Vector3.one;
            }
            else if (weapon.WeaponSlot[0] == WeaponSlot.LeftHip)
            {
                weapon.transform.localPosition = this.offsetPosMeleeHolderLeft;
                weapon.transform.localRotation = Quaternion.Euler(this.offsetRotMeleeHolderLeft);
                weapon.transform.localScale = Vector3.one;
            }
        }
        else
        {
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
            weapon.transform.localScale = Vector3.one;
        }
    }

    public void RemoveWeapon(Weapon weapon)
    {
        if (this.weapons.Count == 0) return;
        this.weapons.Remove(weapon);
    }

    public void SetActiveWeapon(int weaponIndex, bool isAlpha)
    {
        int weaponCount = this.weapons.Count;
        if (isAlpha)
        {
            if (weaponIndex < 0 || weaponIndex >= weaponCount)
            {
                Debug.Log("Don't have that weapon index");
                return;
            }
        }
        else
        {
            weaponIndex = (weaponIndex % weaponCount + weaponCount) % weaponCount;
            //if (weaponIndex >= weaponCount)
            //    weaponIndex = 0;
            //else if (weaponIndex < 0)
            //    weaponIndex = weaponCount - 1;
        }
        if (weaponIndex == this.currentWeaponIndex) return;

        if (this.currentWeaponIndex > -1)
        {
            this.weapons[this.currentWeaponIndex].gameObject.SetActive(false);
        }
        this.weapons[weaponIndex].gameObject.SetActive(true);

        this.currentWeaponIndex = weaponIndex;

        StartCoroutine(this.StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        this.isReadySwap = false;
        yield return new WaitForSeconds(this.swapCooldown);
        this.isReadySwap = true;
    }
}
