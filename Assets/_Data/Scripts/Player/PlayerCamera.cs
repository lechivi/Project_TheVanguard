using UnityEngine;
using Cinemachine;
using Newtonsoft.Json.Bson;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class PlayerCamera : PlayerAbstract
{
    public Transform cameraLookat;
    public CinemachineFreeLook TPSCam;
    public CinemachineVirtualCamera FPSCam;
    public CinemachineCameraOffset CameraOffsetTPS;
    public bool originalTPSCam;
    public Transform player;
    public Vector3 currentRotation;
    public int POV;
    protected override void Awake()
    {
        base.Awake();
        TPSCam.gameObject.SetActive(true);
        originalTPSCam = true;
        FPSCam.gameObject.SetActive(false);

        this.SetCameraTarget();
    }


    /*  public void ChangeCamera()
      {
          if (TPSCam.gameObject.activeInHierarchy == true)
          {
              ChangeFPSCam();
          }

          else if (FPSCam.gameObject.activeInHierarchy == true)
          {
              ChangeTPSCam();
          }
      }*/
    public void HandleCameraOriginal()
    {
        if (TPSCam.gameObject.activeInHierarchy == true && Input.GetMouseButtonDown(1))
        {
            originalTPSCam = true;
        }
        else if (FPSCam.gameObject.activeInHierarchy == true && Input.GetMouseButtonDown(1))
        {
            originalTPSCam = false;
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

    public void ChangeSpeedFPSCam(float xAxis, float yAxis)
    {
        FPSCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = xAxis;
        FPSCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = yAxis;
    }

    public void ChangeSpeedTPSCam(float xAxis, float yAxis)
    {
        TPSCam.m_XAxis.m_MaxSpeed = xAxis;
        TPSCam.m_YAxis.m_MaxSpeed = yAxis;
        TPSCam.m_Lens.FieldOfView = 40;
    }

    public void ChangePOVFPS(int pov)
    {
        FPSCam.m_Lens.FieldOfView = pov;
    }

    public void SetCameraTarget()
    {
        if (this.playerCtrl.Character == null) return;
        this.TPSCam.Follow = this.PlayerCtrl.PlayerTransform;
        this.TPSCam.LookAt = this.PlayerCtrl.Character.TPS_LookAt;
        this.FPSCam.Follow = this.PlayerCtrl.Character.FPS_Follow;
    }
}
