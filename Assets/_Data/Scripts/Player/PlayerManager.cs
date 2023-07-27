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
        

    }
    private void LateUpdate()
    {

    }

    private void FixedUpdate()
    {
        this.playerCtrl.PlayerWeapon.PlayerWeaponActive.HandleFiring();
        this.playerCtrl.PlayerLocomotion.HanldeAllMovementFix();
  
    }
}
