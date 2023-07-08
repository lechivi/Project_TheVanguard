using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering;

public class PlayerAim : PlayerAbstract
{
    public Rig HandLayer;
    public GameObject weapon;
    public float AimDuration = 0.3f;
    
    public Transform AimLookat;
    public Transform AimLookatCam;
    public float distanceLook1D;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (!playerCtrl.PlayerLocomotion.Is1D)
        {
            AimLookat.position = AimLookatCam.position;
        }
        else
        {
            Handle();
        }

        /*if (Input.GetKey(KeyCode.R))
        {
            EquipWeapon();
        }

        else if (Input.GetKeyDown(KeyCode.T))
        {
            UnEquipWeapon();
        }*/
    }

  /*  private void UnEquipWeapon()
    {
        HandLayer.weight = 0;
        weapon.SetActive(false);
    }

    private void EquipWeapon()
    {
        HandLayer.weight += Time.deltaTime / AimDuration;
        weapon.SetActive(true);
    }*/

    private void Handle()
    {
        Vector3 ball = this.playerCtrl.PlayerTransform.position + this.playerCtrl.PlayerTransform.forward * distanceLook1D;
        ball.y = AimLookatCam.position.y;
        AimLookat.position = ball;
    }

   /* private void OnDrawGizmos()
    {
        Vector3 ball = Player.position + Player.forward * distanceLook1D;
        ball.y = AimLookatCam.position.y;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ball, 0.5f);
    }*/
}
