using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponReload : PlayerWeaponAbstract
{
    public Transform leftHand;
    GameObject magazineHand;
    public bool isReload;
    private void Start()
    {
        if (PlayerWeapon.AnimationEvents != null)
        {
            PlayerWeapon.AnimationEvents.AnimationEvent.AddListener(OnAnimationEvent);
        }
    }

    private void Update()
    {
        SetReloadWeapon(false);
    }
    public void SetReloadWeapon(bool button)
    {
        WeaponRaycast weapon = PlayerWeapon.PlayerWeaponManager.GetActiveRaycastWeapon();
        if (weapon)
        {
            if (!button && weapon.currentAmmo <= 0)
            {
                if(weapon.Weapon.WeaponData.WeaponType == WeaponType.Pistol)
                {
                    this.PlayerWeapon.RigAnimator.SetTrigger("reload_Pistol");
                }
                else
                {
                    this.PlayerWeapon.RigAnimator.SetTrigger("reload_weapon");
                }
                isReload = true;
            }
            if (button)
            {
                if (weapon.Weapon.WeaponData.WeaponType == WeaponType.Pistol)
                {
                    this.PlayerWeapon.RigAnimator.SetTrigger("reload_Pistol");
                }
                else
                {
                    this.PlayerWeapon.RigAnimator.SetTrigger("reload_weapon");
                }
                isReload = true;
            }
        }
    }

    public void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "detach_magazine":
                DetachMagazine();
                break;
            case "drop_magazine":
                DropMagazine();
                break;
            case "refill_magazine":
                RefillMagazine();
                break;
            case "attach_magazine":
                AttachMagazine();
                break;
            case "exitdelay_shotgun":
                PlayerWeapon.PlayerWeaponActive.SetisDelay(false);
                break;
        }
    }

    public void DetachMagazine()
    {
        WeaponRaycast weapon = this.PlayerWeapon.PlayerWeaponManager.GetActiveRaycastWeapon();
        magazineHand = Instantiate(weapon.magazine, leftHand, true);
        weapon.magazine.SetActive(false);
    }

    public void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(magazineHand, leftHand.transform.position, leftHand.transform.rotation);
        droppedMagazine.transform.localScale = Vector3.one;
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();
        magazineHand.SetActive(false);
    }

    public void RefillMagazine()
    {
        magazineHand.SetActive(true);
    }

    public void AttachMagazine()
    {
        WeaponRaycast weapon = this.PlayerWeapon.PlayerWeaponManager.GetActiveRaycastWeapon();
        weapon.magazine.SetActive(true);
        Destroy(magazineHand);
        weapon.currentAmmo = weapon.maxAmmo;
        if (weapon.Weapon.WeaponData.WeaponType == WeaponType.Pistol)
        {
            this.PlayerWeapon.RigAnimator.ResetTrigger("reload_Pistol");
        }
        else 
        {
            this.PlayerWeapon.RigAnimator.ResetTrigger("reload_weapon");
        }
        Invoke("ChangeIsReload", 0.15f);
    }

    public void ChangeIsReload()
    {
        isReload = false;
    }

}
