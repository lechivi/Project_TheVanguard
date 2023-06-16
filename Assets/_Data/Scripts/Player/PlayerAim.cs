using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering;

public class PlayerAim : PlayerAbstract
{
    public Rig HandLayer;
    public Rig BodyAim;
    public GameObject weapon;
    public float AimDuration = 0.3f;
    public bool canChange;

    protected override void Awake()
    {
        base.Awake();
        HandLayer.weight = 0f;
        weapon.SetActive(false);
    }

    private void Update()
    {
        if (!playerCtrl.PlayerLocomotion.Is1D)
        {
            BodyAim.weight = 1f;
        }
        else
        {
            BodyAim.weight = 0f;
        }

        if (Input.GetKey(KeyCode.R))
        {
            EquipWeapon();
        }

        else if (Input.GetKeyDown(KeyCode.T))
        {
            UnEquipWeapon();
        }
    }

    private void UnEquipWeapon()
    {
        HandLayer.weight = 0;
        weapon.SetActive(false);
    }

    private void EquipWeapon()
    {
        HandLayer.weight += Time.deltaTime / AimDuration;
        weapon.SetActive(true);
    }
}
