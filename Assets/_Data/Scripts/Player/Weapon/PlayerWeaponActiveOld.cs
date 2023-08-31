using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering.VirtualTexturing;

public class PlayerWeaponActiveOld : PlayerWeaponAbstract
{

    public Transform crossHairTarget;
    private WeaponRaycast weaponRaycast;
    public bool isFiring = false;
    public bool iscanFire;
    private float timedelta = 0;

    protected override void Awake()
    {
        base.Awake();
    }


    private void Update()
    {
        weaponRaycast = PlayerWeapon.PlayerWeaponManager.GetActiveRaycastWeapon();
        SetIsCanFire();
    }

    private void FixedUpdate()
    {
        if (weaponRaycast != null)
        {

        }
    }


    public void HandleFiring()
    {
        if (weaponRaycast == null) return;
        if (weaponRaycast.Weapon.WeaponData.ShotGunType != ShotGunType.Slowhand)
        {
            weaponRaycast.DelayPerShot();
        }
        if (iscanFire)
        {
            if (Input.GetMouseButtonDown(0) && weaponRaycast.Weapon.WeaponData.WeaponType != WeaponType.AssaultRifle)
            {
                PlayerWeapon.PlayerCtrl.PlayerLocomotion.IsWalking = true;
                weaponRaycast.FireBullet(crossHairTarget.position);
                if (weaponRaycast.Weapon.WeaponData.ShotGunType == ShotGunType.Slowhand)
                {
                    DelayShotgun();
                }
            }

            if (isFiring && weaponRaycast.Weapon.WeaponData.WeaponType == WeaponType.AssaultRifle)
            {
                weaponRaycast.UpdateFiring(crossHairTarget.position);
                PlayerWeapon.PlayerCtrl.PlayerLocomotion.IsWalking = true;
            }

            if (!isFiring && weaponRaycast.Weapon.WeaponData.WeaponType == WeaponType.AssaultRifle)
            {
                weaponRaycast.runtTimeFire = 0;
                weaponRaycast.recoil.ResetIndex();
            }
        }
        weaponRaycast.UpdateBullets();

    }

    public void DelayPerShot(float timedelay)
    {
        timedelta += Time.deltaTime;
        if (timedelta < timedelay) return;
        timedelta = 0;
        SetisDelay(false);
    }

    public void DelayShotgun()
    {
        PlayerWeapon.RigAnimator.SetTrigger("reload_pershot");
        timedelta = 0;
        SetisDelay(true);
    }

    public void SetIsCanFire()
    {
        if(weaponRaycast == null) return;
        if (!PlayerWeapon.PlayerWeaponManager.IsHolstering && !PlayerWeapon.PlayerWeaponReload.isReload && !weaponRaycast.isDelay)
        {
            iscanFire = true;
        }
        else
        {
            iscanFire = false;
        }
    }
    public void SetisDelay(bool isDelay)
    {
        if (weaponRaycast)
        {
            weaponRaycast.isDelay = isDelay;
        }
    }
}
