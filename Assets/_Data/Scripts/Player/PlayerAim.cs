using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering;

public class PlayerAim : PlayerAbstract
{
    public GameObject scope;
    public Camera cameraMain;
    public Animator rigLayer;
    public bool isAim;
    public Rig HandLayer;
    public GameObject weapon;
    public float AimDuration = 0.3f;

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
            Handle();
        }
        //// code dưới đây chỉ là test
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(testAimSni(0.2f));
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            cameraMain.cullingMask |= 1 << 7;
            cameraMain.cullingMask |= 1 << 6;
            scope.SetActive(false);
        }
    }


    private void Handle()
    {
        Vector3 ball = this.playerCtrl.PlayerTransform.position + this.playerCtrl.PlayerTransform.forward * distanceLook1D;
        ball.y = AimLookatCam.position.y;
        AimlookMain.position = ball;
    }

    private void AimWeapon()
    {

        rigLayer.SetBool("aim_weapon", isAim);
    }

    //// code dưới đây chỉ là test

    public IEnumerator testAimSni(float second)
    {
        yield return new WaitForSeconds(second);

        cameraMain.cullingMask &= ~(1 << 7);
        cameraMain.cullingMask &= ~(1 << 6);
        scope.SetActive(true);

    }




    /* private void OnDrawGizmos()
     {
         Vector3 ball = Player.position + Player.forward * distanceLook1D;
         ball.y = AimLookatCam.position.y;
         Gizmos.color = Color.red;
         Gizmos.DrawSphere(ball, 0.5f);
     }*/
}
