using System.Collections;
using UnityEngine;

public class PlayerAim : PlayerAbstract
{
    public bool IsAim;

    //[SerializeField] private Transform aimLookMain;
    //[SerializeField] private Transform aimLookAtCam;
    //private float distanceLook1D;

    public void HandleUpdateAim()
    {
        //if (!playerCtrl.PlayerLocomotion.Is1D)
        //{
        //    aimLookMain.position = aimLookAtCam.position;
        //    AimWeapon();
        //}
        //else
        //{
        //    HandleBodyAim1D();
        //}

        if (!IsAim)
        {
            playerCtrl.PlayerCamera.ChangePOVFPS(60);
            if (playerCtrl.PlayerCamera.FPSCamera.enabled)
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
        if (weapon)
        {
            if (isAimInput && !playerCtrl.PlayerWeapon.PlayerWeaponManager.IsHolstering && !playerCtrl.PlayerWeapon.PlayerWeaponReload.IsReload)
            {
                IsAim = true;
            }
            else
            {
                IsAim = false;
            }
        }
    }
    public void AimWeapon()
    {
        WeaponRaycast weapon = playerCtrl.PlayerWeapon.PlayerWeaponManager.GetActiveRaycastWeapon();
        if (weapon)
        {
            playerCtrl.RigAnimator.SetBool("aim_" + weapon.Weapon.WeaponData.WeaponType, IsAim);
            if (weapon && weapon.Weapon.WeaponData.WeaponType == WeaponType.SniperRifle)
            {
                AimWeaponSCope();
                return;
            }
            if (IsAim)
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
        if (!IsAim)
        {
            playerCtrl.PlayerCamera.MainCamera.cullingMask |= 1 << 7;
            playerCtrl.PlayerCamera.MainCamera.cullingMask |= 1 << 6;

            if (UIManager.HasInstance)
            {
                UIManager.Instance.InGamePanel.AlwaysOnUI.Scope.Hide();
            }
        }
    }

    //private void HandleBodyAim1D()
    //{
    //    Vector3 ball = this.playerCtrl.PlayerTransform.position + this.playerCtrl.PlayerTransform.forward * distanceLook1D;
    //    ball.y = aimLookAtCam.position.y;
    //    aimLookMain.position = ball;
    //}

    public IEnumerator AimDuration(float second)
    {
        yield return new WaitForSeconds(second);

        playerCtrl.PlayerCamera.MainCamera.cullingMask &= ~(1 << 7);
        playerCtrl.PlayerCamera.MainCamera.cullingMask &= ~(1 << 6);
        playerCtrl.PlayerCamera.ChangePOVFPS(10);

        if (UIManager.HasInstance)
        {
            UIManager.Instance.InGamePanel.AlwaysOnUI.Scope.Show(null);
        }
    }

}
