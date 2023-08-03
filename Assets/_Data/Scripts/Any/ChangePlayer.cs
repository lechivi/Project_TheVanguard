using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook TPSCamera;
    [SerializeField] private CinemachineVirtualCamera FPSCamera;

    [SerializeField] private GameObject player1;
    [SerializeField] private Transform p1_TPS_LookAt;
    [SerializeField] private Transform p1_FPS_Follow;

    [SerializeField] private GameObject player2;
    [SerializeField] private Transform p2_TPS_LookAt;
    [SerializeField] private Transform p2_FPS_Follow;

    private bool check = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            this.SetActivePlayer(!this.check);
        }
    }

    private void SetActivePlayer(bool check)
    {
        if (check)
        {
            this.player1.transform.position = this.player2.transform.position;
        }
        else
        {
            this.player2.transform.position = this.player1.transform.position;
        }

        this.player1.SetActive(check);
        this.player2.SetActive(!check);

        this.TPSCamera.Follow = check? this.player1.transform : this.player2.transform;
        this.TPSCamera.LookAt = check ? this.p1_TPS_LookAt : this.p2_TPS_LookAt;

        this.FPSCamera.Follow = check? this.p1_FPS_Follow : this.p2_FPS_Follow;

        this.check = check;
    }

}
