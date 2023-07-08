using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class RecoilWeapon : MonoBehaviour
{
    //public PlayerLocomotion playerCam;
    [HideInInspector] public Cinemachine.CinemachineFreeLook playerTPSCam;
    // [HideInInspector] public Cinemachine.CinemachineVirtualCamera playerFPSCam;
    [HideInInspector] public Cinemachine.CinemachineImpulseSource cameraShake;
    [HideInInspector] public Animator rigController;
    public float verticalRecoil;
    public float horizontalRecoil;
    public float duration;
    private float time;
    public Vector2[] recoilPattern;
    public int index;

    protected void Awake()
    {
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
            playerTPSCam.m_YAxis.Value -= ((verticalRecoil / 1000) * Time.deltaTime) / duration;
            playerTPSCam.m_XAxis.Value -= ((horizontalRecoil / 10) * Time.deltaTime) / duration;
            time -= Time.deltaTime;
        }
    }
}
