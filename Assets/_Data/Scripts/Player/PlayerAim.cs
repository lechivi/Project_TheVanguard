using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering;

public class PlayerAim : PlayerAbstract
{

    public float duration;
    public Transform player;
    public GameObject scope;
    public Camera cameraMain;
    public Animator rigLayer;
    public bool isAim;
    public Rig HandLayer;

    public Transform AimlookMain;
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
            AimlookMain.position = AimLookatCam.position;
            AimWeapon();
        }
        else
        {
            HandleBodyAim1D();
        }

        if (!isAim)
        {
            playerCtrl.PlayerCamera.ChangePOVFPS(40);
            if (playerCtrl.PlayerCamera.FPSCam.enabled)
            {
                playerCtrl.PlayerCamera.ChangeSpeedFPSCam(300f, 300f);
            }
            else
            {
                playerCtrl.PlayerCamera.ChangeSpeedTPSCam(300f, 1.5f);
            }
        }

    }

    public void AimWeapon()
    {
        RaycastWeapon raycastWeapon = playerCtrl.PlayerWeapon.PlayerWeaponActive.GetActiveWeapon();
        if (raycastWeapon != null)
        {
            rigLayer.SetBool("aim_" + raycastWeapon.Weapon.WeaponData.WeaponType, isAim);
            if (raycastWeapon && raycastWeapon.Weapon.WeaponData.WeaponType == WeaponType.SniperRifle)
            {
                AimWeaponSCope();
                return;
            }
            if(isAim)
            {
                playerCtrl.PlayerCamera.ChangePOVFPS(35);
            }
        }
    }

    private void AimWeaponSCope()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(AimDuration(0.2f));

        }
        if (!isAim)
        {
            cameraMain.cullingMask |= 1 << 7;
            cameraMain.cullingMask |= 1 << 6;
            scope.SetActive(false);
        }
    }
    private void HandleBodyAim1D()
    {
        Vector3 ball = this.playerCtrl.PlayerTransform.position + this.playerCtrl.PlayerTransform.forward * distanceLook1D;
        ball.y = AimLookatCam.position.y;
        AimlookMain.position = ball;
    }


    public IEnumerator AimDuration(float second)
    {
        yield return new WaitForSeconds(second);

        cameraMain.cullingMask &= ~(1 << 7);
        cameraMain.cullingMask &= ~(1 << 6);
        scope.SetActive(true);
        playerCtrl.PlayerCamera.ChangePOVFPS(10);
    }

}
