using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeaponNew : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazinesize, bulletPertap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

}
