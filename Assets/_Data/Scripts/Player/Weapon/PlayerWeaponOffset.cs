using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponOffset : PlayerWeaponAbstract
{
    private void Update()
    {
        transform.localRotation = Quaternion.Euler(3, 5, 8);
    }
}
