using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbstract : MonoBehaviour
{
    protected PlayerCtrl playerCtrl;

    protected virtual void Awake()
    {
        this.playerCtrl = GetComponent<PlayerCtrl>();
    }
}
