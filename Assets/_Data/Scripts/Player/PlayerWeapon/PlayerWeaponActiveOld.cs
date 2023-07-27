using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerWeaponActiveOld : PlayerWeaponAbstract
{
    public enum Weaponslot
    {
        Primary = 0,
        Secondary = 1,

    }

    public Transform crossHairTarget;
    public GameObject weapon;
    public RaycastWeapon[] equipped_weapon = new RaycastWeapon[2];
    public int ActiveWeaponIndex;
    public bool isFiring = false;
    public Transform[] weaponSlots;
    public Animator rigController;
    public bool isHolster;
    public PlayerCamera playerCamera;
    public bool iscanFire;

    protected override void Awake()
    {
        base.Awake();
        weapon = GameObject.FindGameObjectWithTag("Gun");
    }

    private void Start()
    {
        ActiveWeaponIndex = -1;
        if (weapon)
        {
            RaycastWeapon weaponraycast = weapon.GetComponent<RaycastWeapon>();
            Equip(weaponraycast);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleActiveWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(Weaponslot.Primary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(Weaponslot.Secondary);
        }
        CanfireCondition();
        //Debug.Log(PlayerWeapon.PlayerCtrl.PlayerLocomotion.IsSprinting && !PlayerWeapon.PlayerWeaponReload.isReload);
    }

    public void CanfireCondition()
    {
        if ( !isHolster && !PlayerWeapon.PlayerWeaponReload.isReload)
        {
            iscanFire = true;
        }
        else
        {
            iscanFire = false;
        }
    }
    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeapon(ActiveWeaponIndex);
    }

    private RaycastWeapon GetWeapon(int index)
    {
        if (index < 0 || index >= equipped_weapon.Length)
        {
            return null;
        }
        return equipped_weapon[index];
    }
    public void HandleFiring()
    {
        var weaponRaycast = GetWeapon(ActiveWeaponIndex);
        if (weaponRaycast && iscanFire)
        {
            if (Input.GetMouseButtonDown(0))
            {
                weaponRaycast.FireBullet(crossHairTarget.position);
                Debug.Log("Fire");
            }
            if (weaponRaycast.WeaponType == WeaponType.Shotgun)
            {
                weaponRaycast.UpdateBullets();
                return;
            }

            //
            if (isFiring)
            {
                weaponRaycast.UpdateFiring(crossHairTarget.position);

            }

            weaponRaycast.UpdateBullets();
            if (!isFiring)
            {
                weaponRaycast.runtTimeFire = 0;
                weaponRaycast.recoil.ResetIndex();
            }
        }

    }

    private void ToggleActiveWeapon()
    {
        bool isHolster = rigController.GetBool("holster_weapon");
        if (isHolster)
        {
            StartCoroutine(ActivateWeapon(ActiveWeaponIndex));
        }
        else
        {
            Debug.Log("hello");
            StartCoroutine(HolsterWeapon(ActiveWeaponIndex));
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int index = (int)newWeapon.weaponslot;
        var originalweaponRaycast = GetWeapon(ActiveWeaponIndex);
        var weaponRaycast = GetWeapon(index);
        /* if (weaponRaycast)
         {
             //Destroy(weaponRaycast.gameObject);
         }*/

        if (index == ActiveWeaponIndex)
        {
            originalweaponRaycast.ammo = weaponRaycast.maxAmmo;
            return;
        }
        Debug.Log("newWeapon");
        weaponRaycast = newWeapon;
        // weaponRaycast.transform.parent = weaponSlots[index];
        weaponRaycast.transform.SetParent(weaponSlots[index], false);
        weaponRaycast.recoil.playerTPSCam = playerCamera.TPSCam;
        weaponRaycast.recoil.playerFPSCam = playerCamera.FPSCam;
        weaponRaycast.recoil.rigController = rigController;

        // weaponRaycast.transform.localPosition = Vector3.zero;
        // weaponRaycast.transform.localRotation = Quaternion.identity;
        // rigController.Play("equip_" + weaponRaycast.weaponName);
        equipped_weapon[index] = weaponRaycast;
        ActiveWeaponIndex = index;
        SetActiveWeapon(newWeapon.weaponslot);
    }

    private void SetActiveWeapon(Weaponslot weaponSlotIndex)
    {
        int holsterIndex = ActiveWeaponIndex;
        int activateIndex = (int)weaponSlotIndex;
        if (holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }
        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    private IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        ActiveWeaponIndex = activateIndex;
    }

    IEnumerator HolsterWeapon(int index)
    {
        isHolster = true;
        var weaponRaycast = GetWeapon(index);
        if (weaponRaycast)
        {
            rigController.SetBool("holster_weapon", true);
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

        }
    }

    IEnumerator ActivateWeapon(int index)
    {
        var weaponRaycast = GetWeapon(index);
        if (weaponRaycast)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + weaponRaycast.weaponName);
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            isHolster = false;
        }
    }
}
