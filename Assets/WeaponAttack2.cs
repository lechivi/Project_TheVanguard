using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack2 : MonoBehaviour
{
    public ParticleSystem bom;
    float x;
    float y;
    float z;
    private void Start()
    {
        bom.gameObject.transform.localScale = Vector3.zero;
    }
    private void Update()
    {
        x += (float)0.1 * Time.deltaTime;
        y += (float)0.1 * Time.deltaTime;
        z += (float)0.1 * Time.deltaTime;
        bom.gameObject.transform.localScale = new Vector3(x, y, z);
        if(Input.GetMouseButtonDown(0))
        {
            bom.Play();
        }
    }
}
