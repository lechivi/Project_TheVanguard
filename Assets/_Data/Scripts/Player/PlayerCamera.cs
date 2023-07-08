using UnityEngine;

public class PlayerCamera : PlayerAbstract
{
    public GameObject TPSCam;
    public GameObject FPSCam;
    protected override void Awake()
    {
        TPSCam.SetActive(true);
        FPSCam.SetActive(false);
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
        if (TPSCam.activeInHierarchy == true)
        {
            TPSCam.SetActive(false);
            FPSCam.SetActive(true);
            playerCtrl.PlayerLocomotion.Is1D = false;
        }

        else if (FPSCam.activeInHierarchy == true)
        {
            FPSCam.SetActive(false);
            TPSCam.SetActive(true);
        }
    }
}
