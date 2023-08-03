using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayShotgun :MonoBehaviour, IDelayShot
{
    public void DelayShot()
    {
        Debug.Log("Shotgun");
    }

    public WeaponType GetWeaponType()
    {
        return WeaponType.Shotgun;
    }
}
