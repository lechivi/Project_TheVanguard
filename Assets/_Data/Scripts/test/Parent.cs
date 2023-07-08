using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    protected virtual void Awake()
    {
        Debug.Log("Parent");
    }

    protected virtual void ParentFunction()
    {
        Debug.Log("Parent Function");
    }
}
