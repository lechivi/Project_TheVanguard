using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapons : PickupObject
{
    protected Weapon weaponPrefab;

    protected override void Awake()
    {
        base.Awake();

        int pickupObjectLayer = LayerMask.NameToLayer("PickupObject");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics.IgnoreLayerCollision(pickupObjectLayer, playerLayer, true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.weaponPrefab == null) return;
        Debug.Log("Trigger");
        PlayerWeaponManager weaponManager = other.GetComponentInChildren<PlayerWeaponManager>();
        if (weaponManager)
        {
            //Weapon weapon = Instantiate(this.weaponPrefab);
            bool canAdd = weaponManager.AddWeapon(this.weaponPrefab);
            if (canAdd)
            {
                //transform.parent.gameObject.SetActive(false);
                Destroy(transform.parent.gameObject);
            }
        }
    }

    public void SetWeapon(Weapon weapon) //Spawner Call
    {
        this.weaponPrefab = weapon;
        this.GenerateModelObject();
    }

    protected override void GenerateModelObject()
    {
        base.GenerateModelObject();
        if (this.weaponPrefab == null) return;

        GameObject model = Instantiate(weaponPrefab.WeaponData.Model);
        model.transform.SetParent(this.virtualObject.transform);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
        model.transform.localScale = Vector3.one;

        Collider collider = model.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }
}
