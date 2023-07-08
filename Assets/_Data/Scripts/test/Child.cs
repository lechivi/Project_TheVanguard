using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : AbstractClasas
{
    protected override void Awake()
    {
        base.Awake();
        ParentFunction();
    }

    protected override void ParentFunction()
    {
        base.ParentFunction();
    }
}
