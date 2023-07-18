using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOnRender : MonoBehaviour
{

    public Camera mainCamera;
   // public LayerMask gunLayer;

    private void Update()
    {
        // Nhấn phím "O" để loại bỏ layer súng khỏi culling mask của camera
        if (Input.GetKeyUp(KeyCode.M))
        {
            // Loại bỏ layer súng khỏi culling mask của camera
            mainCamera.cullingMask &= ~(1 << 7);
        }

        if(Input.GetKeyUp(KeyCode.N))
        {
            mainCamera.cullingMask |= 1 << 7;
        }
    }
}
