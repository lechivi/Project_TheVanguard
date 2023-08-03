using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAK12 : MonoBehaviour, IDelayShot
{
    public void DelayShot()
    {
        Debug.Log("AK12");
    }

    public WeaponType GetWeaponType()
    {
        return WeaponType.AssaultRifle;
    }
}
