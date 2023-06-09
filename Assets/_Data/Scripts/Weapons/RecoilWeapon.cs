using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class RecoilWeapon : MonoBehaviour
{
    public NoiseSettings FPSNoiseSetting;
    public NoiseSettings TPSNoiseSetting;
    public CinemachineImpulseSource CinemachineImpulse;
    //public PlayerLocomotion playerCam;
    [HideInInspector] public Cinemachine.CinemachineFreeLook playerTPSCam;
    [HideInInspector] public Cinemachine.CinemachineVirtualCamera playerFPSCam;
    [HideInInspector] public Cinemachine.CinemachineImpulseSource cameraShake;
    [HideInInspector] public Animator rigController;
    public float verticalRecoil;
    public float horizontalRecoil;
    public float duration;
    private float time;
    public Vector2[] recoilPattern;
    public int index;
    public int recoilFPS;

    protected void Awake()
    {
        CinemachineImpulse = GetComponent<CinemachineImpulseSource>();
        cameraShake = GetComponent<CinemachineImpulseSource>();
    }

    public void ResetIndex()
    {
        index = 0;
    }


    public int NextIndex(int index)
    {
        return (index + 1) % recoilPattern.Length;
    }
    public void GenerateRecoil(string weaponName)
    {
        time = duration;
        cameraShake.GenerateImpulse(Camera.main.transform.forward);
        horizontalRecoil = recoilPattern[index].x;
        verticalRecoil = recoilPattern[index].y;
        index = NextIndex(index);
        rigController.Play("WeaponRecoil_" + weaponName, 1, 0.0f);
    }
    private void Update()
    {
        //  Debug.Log(index);
        if (time > 0)
        {
            if(playerTPSCam.gameObject.activeInHierarchy == true)
            {
                TPSRecoil();
                ChangeNoiseSetiing(TPSNoiseSetting);
            }
            if (playerFPSCam.gameObject.activeInHierarchy == true)
            {
                FPSRecoil();
                ChangeNoiseSetiing(FPSNoiseSetting);
            }
        }
    }

    private void TPSRecoil()
    {
        playerTPSCam.m_YAxis.Value -= ((verticalRecoil / 1000) * Time.deltaTime) / duration;
        playerTPSCam.m_XAxis.Value -= ((horizontalRecoil / 10) * Time.deltaTime) / duration;
        time -= Time.deltaTime;
    }

    private void FPSRecoil()
    {
        playerFPSCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value -= ((verticalRecoil /20) * Time.deltaTime) / duration;
        playerFPSCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value -= ((horizontalRecoil / 10) * Time.deltaTime) / duration;
        time -= Time.deltaTime;
        Debug.Log("Hello");
    }

    public void ChangeNoiseSetiing(NoiseSettings newNoiseSetiing)
    {
        CinemachineImpulse.m_ImpulseDefinition.m_RawSignal = newNoiseSetiing;
    }
}
