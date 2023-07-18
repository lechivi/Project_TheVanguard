using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponReload : PlayerWeaponAbstract
{
    [SerializeField] private PlayerRigAnimationEvents animationEvents;
    public Transform leftHand;
    GameObject magazineHand;
    public bool isReload;
    private void Start()
    {
        if (this.animationEvents != null)
        {
            animationEvents.AnimationEvent.AddListener(OnAnimationEvent);
        }
    }

    public void SetReloadWeapon()
    {

        this.PlayerWeapon.RigAnimator.SetTrigger("reload_weapon");
        isReload = true;
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

        }
    }

    public void DetachMagazine()
    {
        RaycastWeapon weapon = this.PlayerWeapon.PlayerWeaponActive.GetActiveWeapon();
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
        RaycastWeapon weapon = this.PlayerWeapon.PlayerWeaponActive.GetActiveWeapon();
        weapon.magazine.SetActive(true);
        Destroy(magazineHand);
        weapon.ammo = weapon.maxAmmo;
        this.PlayerWeapon.RigAnimator.ResetTrigger("reload_weapon");
        isReload = false;
    }
}
