using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickupWeapon : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    [SerializeField] private GameObject pickupObjectPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (this.pickupObjectPrefab == null) return;

            GameObject newPickupObject = Instantiate(pickupObjectPrefab, this.transform);
            newPickupObject.transform.localPosition = Vector3.zero + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

            PickupWeapons pickupWeapon = newPickupObject.GetComponentInChildren<PickupWeapons>();
            pickupWeapon.SetWeapon(this.weapons[Random.Range(0, weapons.Count)]);
            
        }
    }
}
