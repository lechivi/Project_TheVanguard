using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : PlayerAbstract
{
    private void Update()
    {
        this.playerCtrl.PlayerInput.HandleAllInput();
        this.playerCtrl.PlayerLocomotion.HanldeAllMovementUpdate();
        if (this.playerCtrl.PlayerWeapon.PlayerWeaponActive == null) return;
        this.playerCtrl.PlayerWeapon.PlayerWeaponActive.HandleFiring();
    }

    private void LateUpdate()
    {

    }

    private void FixedUpdate()
    {
        this.playerCtrl.PlayerLocomotion.HanldeAllMovementFix();
        //this.playerCtrl.PlayerWeapon.PlayerWeaponActive.HandleFiring();
    }
}
