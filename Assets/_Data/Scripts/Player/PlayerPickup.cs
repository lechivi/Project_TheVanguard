using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : PlayerAbstract
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private float pickupDistance = 7.5f;

    private bool canPickup = false;

    private void Update()
    {
        //if (Physics.Raycast(this.playerCamera.transform.position, this.playerCamera.transform.forward, out RaycastHit hitInfo, this.pickupDistance, this.pickupLayer))
        //{
        //    Debug.Log("(E) Pickup: " + hitInfo.transform.name);
        //    this.canPickup = true;
        //}
        //else
        //{
        //    this.canPickup= false;
        //}

        //if (Input.GetKeyDown(KeyCode.E) && this.canPickup)
        //{
        //    if (hitInfo.collider != null && hitInfo.transform.TryGetComponent(out PickupWeapons pickupWeapons))
        //    {
        //        pickupWeapons.Pickup(this.playerCtrl.PlayerWeapon.PlayerWeaponManager);
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(this.playerCamera.transform.position, this.playerCamera.transform.position + (this.playerCamera.transform.forward * this.pickupDistance));
    }
}
