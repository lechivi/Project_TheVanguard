using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAim : PlayerAbstract
{
    public float test;
    public GameObject scope;
    public bool isAim;

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
            playerCtrl.PlayerCamera.ChangePOVFPS(60);
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

    public void SetIsAim(bool isAimInput)
    {
        WeaponRaycast weapon = playerCtrl.PlayerWeapon.PlayerWeaponManager.GetActiveRaycastWeapon();
        if(weapon)
        {
            if(isAimInput && !playerCtrl.PlayerWeapon.PlayerWeaponManager.IsHolstering && !playerCtrl.PlayerWeapon.PlayerWeaponReload.isReload)
            {
                isAim = true;
            }
            else
            {
                isAim = false;
            }
        }
    }
    public void AimWeapon()
    {
        WeaponRaycast weapon = playerCtrl.PlayerWeapon.PlayerWeaponManager.GetActiveRaycastWeapon();
        if (weapon && !playerCtrl.PlayerWeapon.PlayerWeaponManager.IsHolstering)
        {
            playerCtrl.RigAnimator.SetBool("aim_"  + weapon.Weapon.WeaponData.WeaponType, isAim);
            if (weapon && weapon.Weapon.WeaponData.WeaponType == WeaponType.SniperRifle)
            {
                AimWeaponSCope();
                return;
            }
            if (isAim)
            {
                playerCtrl.PlayerLocomotion.IsWalking = true;
                playerCtrl.PlayerCamera.ChangePOVFPS(55);
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
            playerCtrl.PlayerCamera.cameraMain.cullingMask |= 1 << 7;
            playerCtrl.PlayerCamera.cameraMain.cullingMask |= 1 << 6;
            if (scope)
            {
                scope.SetActive(false);
            }
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

        playerCtrl.PlayerCamera.cameraMain.cullingMask &= ~(1 << 7);
        playerCtrl.PlayerCamera.cameraMain.cullingMask &= ~(1 << 6);
        if (scope) { scope.SetActive(true); }
        playerCtrl.PlayerCamera.ChangePOVFPS(10);
    }

}
