using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPosition : MonoBehaviour
{
    void Update()
    {
        Debug.Log("PosY: " + transform.position.y);
    }
}
