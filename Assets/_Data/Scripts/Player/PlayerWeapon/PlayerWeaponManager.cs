using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : PlayerWeaponAbstract
{
    [SerializeField] private int maxEquippedWeapon = 3;
    [SerializeField] private int maxInventoryWeapon = 2;
    [SerializeField] private NullAwareList<Weapon> equippedWeapons = new NullAwareList<Weapon>();
    [SerializeField] private NullAwareList<Weapon> inventoryWeapons = new NullAwareList<Weapon>();
    [SerializeField] private Transform[] weaponHolderSlots = new Transform[4];

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform dropPoint;
    [SerializeField] private float dropForce = 10f;
    [SerializeField] private GameObject pickupObjectPrefab;

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

        if (Input.GetKeyDown(KeyCode.L))
        {
            this.RemoveWeaponFromEquipped(this.equippedWeapons.GetList()[0]);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            this.RemoveWeaponFromInventory(this.inventoryWeapons.GetList()[0]);
        }
    }

    public bool AddWeapon(Weapon weapon)
    {
        if (this.equippedWeapons.GetList().Count < this.maxEquippedWeapon || (this.equippedWeapons.ContainsNull() && this.equippedWeapons.GetList().Count <= this.maxEquippedWeapon))
        {
            Weapon weaponAdded = Instantiate(weapon);
            this.AddWeaponToEquipped(weaponAdded);
            return true;
        }
        if (this.inventoryWeapons.GetList().Count < this.maxInventoryWeapon || (this.inventoryWeapons.ContainsNull() && this.inventoryWeapons.GetList().Count <= this.maxInventoryWeapon))
        {
            Weapon weaponAdded = Instantiate(weapon);
            this.AddWeaponToInventory(weaponAdded);
            return true;
        }
        Debug.Log("Can't hold any more weapon");
        return false;
    }

    private void AddWeaponToEquipped(Weapon weapon)
    {
        this.equippedWeapons.Add(weapon);

        this.SetHolsterForWeapon(weapon);

        if (this.currentWeaponIndex == -1)
        {
            this.SetActiveWeapon(this.equippedWeapons.GetList().IndexOf(weapon), false);
        }
        else
        {
            weapon.gameObject.SetActive(false);
        }
    }

    public void AddWeaponToInventory(Weapon weapon)
    {
        this.inventoryWeapons.Add(weapon);

        this.SetHolsterForWeapon(weapon);
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

        weapon.gameObject.SetActive(false);
    }


    public void RemoveWeaponFromEquipped(Weapon weapon)
    {
        this.equippedWeapons.Remove(weapon);

        this.DropWeapon(weapon);
        Destroy(weapon.transform.gameObject);

    }

    public void RemoveWeaponFromInventory(Weapon weapon)
    {
        this.inventoryWeapons.Remove(weapon);

        this.DropWeapon(weapon);
        Destroy(weapon.transform.gameObject);
    }

    private void DropWeapon(Weapon weapon)
    {
        GameObject pickupObject = Instantiate(this.pickupObjectPrefab, this.spawnPoint);
        pickupObject.transform.position = this.dropPoint.position;
        pickupObject.transform.rotation = this.dropPoint.rotation;

        Weapon droppedWeapon = Instantiate(weapon);
        PickupWeapons pickupWeapon = pickupObject.GetComponentInChildren<PickupWeapons>();
        pickupWeapon.SetWeapon(droppedWeapon);

        Rigidbody weaponRigidbody = pickupObject.transform.GetComponent<Rigidbody>();
        if (weaponRigidbody != null)
        {
            weaponRigidbody.isKinematic = true;
            //weaponRigidbody.AddForce(this.dropPoint.forward * this.dropForce, ForceMode.Impulse);
            //Debug.Log("AddForce", weaponRigidbody.gameObject);
        }

        Debug.Log("SpawnPosY: " + pickupObject.transform.position.y);
    }

    public void SetActiveWeapon(int weaponIndex, bool isAlpha)
    {
        List<Weapon> listEquippedWeapon = this.equippedWeapons.GetList();
        int weaponCount = listEquippedWeapon.Count;

        if (weaponCount == 0) return;

        if (isAlpha)
        {
            if (weaponIndex < 0 || weaponIndex >= weaponCount || listEquippedWeapon[weaponIndex] == null)
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
        if (weaponIndex == this.currentWeaponIndex && listEquippedWeapon[weaponIndex] == null) return;

        if (this.currentWeaponIndex > -1 && listEquippedWeapon[currentWeaponIndex] != null)
        {
            listEquippedWeapon[this.currentWeaponIndex].gameObject.SetActive(false);
        }
        listEquippedWeapon[weaponIndex].gameObject.SetActive(true);

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
