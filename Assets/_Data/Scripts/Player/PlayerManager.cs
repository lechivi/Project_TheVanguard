using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : PlayerAbstract
{
    private void Update()
    {
        this.playerCtrl.PlayerInput.HandleAllInput();
       // this.playerCtrl.PlayerLocomotion.HandleFiring();

    }
    private void LateUpdate()
    {
       //this.playerCtrl.PlayerLocomotion.HandleFiring();
    }

    private void FixedUpdate()
    {
        this.playerCtrl.PlayerLocomotion.HanldeAllMovement();
        this.playerCtrl.PlayerWeapon.PlayerWeaponActive.HandleFiring();
    }
}
