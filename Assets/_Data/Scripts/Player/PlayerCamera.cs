using UnityEngine;

public class PlayerCamera : PlayerAbstract
{
    [SerializeField] private Transform Aimlookat;
    public GameObject TPSCam;
    public GameObject FPSCam;

    private float originZ;

    private void Awake()
    {
        TPSCam.SetActive(true);
        FPSCam.SetActive(false);
       // originZ = this.Aimlookat.transform.position.z;

    }

    private void Update()
    {
        Debug.Log(playerCtrl.PlayerInput.MovementInput.y);
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

    public void ChangeAimLookatCam1D()
    {
       // Vector3 Aimlookat1D = new Vector3(0, 0, 20);
        if (playerCtrl.PlayerInput.MovementInput.y>0)
        {

            Aimlookat.transform.localPosition = new Vector3(0, 0, 20);
        }
        else if (playerCtrl.PlayerInput.MovementInput.y < 0)
        {

            Aimlookat.transform.localPosition = new Vector3(0, 0, -20);
        }

        //  this.Aimlookat.position.z = is1D ? originZ : -originZ;
    }
}
