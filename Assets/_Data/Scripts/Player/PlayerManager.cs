using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : PlayerAbstract
{
    private void Update()
    {
        this.playerCtrl.PlayerInput.HandleAllInput();
        
    }

    private void FixedUpdate()
    {
        this.playerCtrl.PlayerLocomotion.HanldeAllMovement();
    }
}
