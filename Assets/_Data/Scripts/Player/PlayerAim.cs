using Cinemachine.Utility;
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
      /*  if (Input.GetMouseButtonDown(0))
        {
            Invoke("reload_pershot", 0.1f);
        }*/
        if (!playerCtrl.PlayerLocomotion.Is1D)
        {
            RaycastWeapon raycastWeapon = playerCtrl.PlayerWeapon.PlayerWeaponActive.GetActiveWeapon();
            AimlookMain.position = AimLookatCam.position;
            AimWeaponAnimator();
            if(raycastWeapon && raycastWeapon.WeaponType == WeaponType.SniperRifle)
            {
                AimWeaponSCope();
            }
        }
        else
        {
            HandleBodyAim1D();
        }

    }


    private void HandleBodyAim1D()
    {
        Vector3 ball = this.playerCtrl.PlayerTransform.position + this.playerCtrl.PlayerTransform.forward * distanceLook1D;
        ball.y = AimLookatCam.position.y;
        AimlookMain.position = ball;
    }

    private void AimWeaponAnimator()
    {
        rigLayer.SetBool("aim_weapon", isAim);
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

    public IEnumerator AimDuration(float second)
    {
        yield return new WaitForSeconds(second);

        cameraMain.cullingMask &= ~(1 << 7);
        cameraMain.cullingMask &= ~(1 << 6);
        scope.SetActive(true);
    }

    public void reload_pershot()
    { // only type ShotGunSuper
        if (playerCtrl.PlayerWeapon.PlayerWeaponReload.isReload) return;
        rigLayer.SetTrigger("reload_pershot");
    }
}
