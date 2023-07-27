using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;

    private PickupWeapons pickupWeapon;

    private void Awake()
    {
        this.pickupWeapon = GetComponent<PickupWeapons>();
    }

    public void Interact(Transform interactorTransfrom)
    {
        PlayerWeaponManager playerWeaponManager = interactorTransfrom.parent.GetComponentInChildren<PlayerWeaponManager>();
        if (playerWeaponManager != null)
        {
            this.pickupWeapon.Pickup(playerWeaponManager);
        }
    }

    public string GetInteractableText()
    {
        this.interactText = "Pick up " + this.pickupWeapon.Weapon.WeaponData.WeaponName;
        return this.interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool CanInteract()
    {
        return true;
    }
}
