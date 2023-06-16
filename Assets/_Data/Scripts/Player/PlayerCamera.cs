using UnityEngine;

public class PlayerCamera : PlayerAbstract
{
    [SerializeField] private Transform Aimlookat;
    public GameObject TPSCam;
    public GameObject FPSCam;

    public float duong;
    public float am;
    float y = 0;
    protected override void Awake()
    {
        TPSCam.SetActive(true);
        FPSCam.SetActive(false);
    }

    private void Update()
    {
        Debug.Log(playerCtrl.PlayerInput.MovementInput.y);
        float x = 0.3f;
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
