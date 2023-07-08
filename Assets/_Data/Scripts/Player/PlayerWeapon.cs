using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : PlayerAbstract
{
    public PlayerWeaponActiveOld PlayerWeaponActive;
    public PlayerWeaponReload PlayerWeaponReload;

    public Animator RigAnimator;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadActiveWeapon();
        this.LoadReloadWeapon();
    }

    protected virtual void LoadActiveWeapon()
    {
        if (this.PlayerWeaponActive == null)
        {
            this.PlayerWeaponActive = GetComponentInChildren<PlayerWeaponActiveOld>();
            Debug.LogWarning(gameObject.name + ": LoadActiveWeapon", gameObject);
        }
    }

    protected virtual void LoadReloadWeapon()
    {
        if (this.PlayerWeaponReload == null)
        {
            this.PlayerWeaponReload = GetComponentInChildren<PlayerWeaponReload>();
            Debug.LogWarning(gameObject.name + ": LoadReloadWeapon", gameObject);
        }
    }
}
