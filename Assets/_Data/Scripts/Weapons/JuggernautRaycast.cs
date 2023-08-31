using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

/*public class JuggernautRaycast : WeaponRaycast
{
    public Transform CrossHairTarget;
    public Transform originalPoint;
    public GameObject Volume;
    public LineRenderer line;

    private void Awake()
    {
        line.enabled = false;
        Volume.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet(CrossHairTarget.position);
            Volume.gameObject.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            line.enabled = false;
            Volume.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        UpdateBullets();
        Ray ray = new(originalPoint.position, CrossHairTarget.position - originalPoint.position);
        if (Physics.Raycast(ray, out hitInfo, 1000f))
        {
            line.SetPosition(0, originalPoint.position);
            line.SetPosition(1, hitInfo.point);
        }
      *//*  else
        {
            line.SetPosition(0, originalPoint.position);
            Vector3 vector3 = originalPoint.position + originalPoint.forward * 1000f;
            line.SetPosition(1, vector3);
        }*//*

    }

    public override void FireBullet(Vector3 target)
    {
        base.FireBullet(target);
        line.enabled = true;
        Debug.Log("RunJunggerNaut");
    }*/



