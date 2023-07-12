using UnityEngine;
using Cinemachine;

public class PlayerCamera : PlayerAbstract
{
    public Transform cameraLookat;
    public AxisState xAxis;
    public AxisState yAxis;
    public CinemachineVirtualCamera TPSCam;
    public CinemachineVirtualCamera FPSCam;
    protected override void Awake()
    {
        TPSCam.gameObject.SetActive(true);
        FPSCam.gameObject.SetActive(false);
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
