using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : PlayerAbstract
{
    public PlayerWeaponManager PlayerWeaponManager;
    public PlayerWeaponActiveOld PlayerWeaponActive;
    public PlayerWeaponReload PlayerWeaponReload;
    public PlayerRigAnimationEvents AnimationEvents;
    public Animator RigAnimator;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerWeaponManager();
        this.LoadPlayerWeaponActive();
        this.LoadPlayerWeaponReload();
        this.LoadRigAnimator();
        this.LoadPlayerRigAnimationEvents();
    }

    protected virtual void LoadPlayerWeaponManager()
    {
        if (this.PlayerWeaponManager == null)
        {
            this.PlayerWeaponManager = GetComponentInChildren<PlayerWeaponManager>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerWeaponManager", gameObject);
        }
    }

    protected virtual void LoadPlayerWeaponActive()
    {
        if (this.PlayerWeaponActive == null)
        {
            this.PlayerWeaponActive = GetComponentInChildren<PlayerWeaponActiveOld>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerWeaponActive", gameObject);
        }
    }

    protected virtual void LoadPlayerWeaponReload()
    {
        if (this.PlayerWeaponReload == null)
        {
            this.PlayerWeaponReload = GetComponentInChildren<PlayerWeaponReload>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerWeaponReload", gameObject);
        }
    }

    protected virtual void LoadRigAnimator()
    {
        if (this.RigAnimator == null)
        {
            this.RigAnimator = this.playerCtrl.RigAnimator;
            Debug.LogWarning(gameObject.name + ": LoadRigAnimator", gameObject);
        }
    }

    protected virtual void LoadPlayerRigAnimationEvents()
    {
        if (this.AnimationEvents == null)
        {
            this.AnimationEvents = this.playerCtrl.RigAnimator.GetComponent<PlayerRigAnimationEvents>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerRigAnimationEvents", gameObject);
        }
    }
}
