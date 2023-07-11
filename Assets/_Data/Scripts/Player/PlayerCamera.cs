using UnityEngine;
using Cinemachine;

public class PlayerCamera : PlayerAbstract
{
    public CinemachineFreeLook TPSCam;
    public CinemachineVirtualCamera FPSCam;
    protected override void Awake()
    {
        TPSCam.gameObject.SetActive(true);
        FPSCam.gameObject.SetActive(false);
    }

    private void Update()
    {/*
        if(playerCtrl.PlayerLocomotion.Is1D)
        {
            if(playerCtrl.PlayerInput.MovementInput.y > 0)
            {
                Aimlookat.transform.localPosition = new Vector3(0, 0, 20);
            }
            else if(playerCtrl.PlayerInput.MovementInput.y < 0)
            {
                Aimlookat.transform.localPosition = new Vector3(0, 0, -20);
            }
        }*/
    }
    public void ChangeCamera()
    {
        if (TPSCam.gameObject.activeInHierarchy == true)
        {
            TPSCam.gameObject.SetActive(false);
            FPSCam.gameObject.SetActive(true);
            playerCtrl.PlayerLocomotion.Is1D = false;
        }

        else if (FPSCam.gameObject.activeInHierarchy == true)
        {
            FPSCam.gameObject.SetActive(false);
            TPSCam.gameObject.SetActive(true);
        }
    }
}
