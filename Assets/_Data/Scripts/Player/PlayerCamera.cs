using UnityEngine;
using Cinemachine;
using Newtonsoft.Json.Bson;
using Unity.VisualScripting;

public class PlayerCamera : PlayerAbstract
{
    public Transform cameraLookat;
    public AxisState xAxis;
    public AxisState yAxis;
    public CinemachineFreeLook TPSCam;
    public CinemachineVirtualCamera FPSCam;
    public bool originalTPSCam;
    protected override void Awake()
    {
        TPSCam.gameObject.SetActive(true);
        FPSCam.gameObject.SetActive(false);
    }

    public void ChangeCamera()
    {
        if (TPSCam.gameObject.activeInHierarchy == true)
        {
            ChangeFPSCam();
        }

        else if (FPSCam.gameObject.activeInHierarchy == true)
        {
            ChangeTPSCam();
        }
    }
    public void HandleCameraAim()
    {
        if (TPSCam.gameObject.activeInHierarchy == true && Input.GetMouseButtonDown(1))
        {
            originalTPSCam = true;
            // ChangeFPSCam();
        }
        else if (FPSCam.gameObject.activeInHierarchy == true && Input.GetMouseButtonDown(1))
        {
            originalTPSCam = false;
           // ChangeFPSCam();
        }
    }

    public void ChangeFPSCam()
    {
        playerCtrl.PlayerLocomotion.Is1D = false;
        TPSCam.gameObject.SetActive(false);
        FPSCam.gameObject.SetActive(true);
    }

    public void ChangeTPSCam()
    {
        TPSCam.gameObject.SetActive(true);
        FPSCam.gameObject.SetActive(false);
    }

    private void Update()
    {
       // Debug.Log(originalTPSCam);
    }
}
