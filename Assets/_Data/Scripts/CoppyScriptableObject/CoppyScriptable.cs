using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoppyScriptable : SaiMonoBehaviour
{
    public WeaponDataSO Source;
    public WeaponDataSO isCoppy;
    public int d;
    public int c;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        // this.CoppyScriptableObj();
        //this.Handle();
    }

    private void Start()
    {
        c = d;
    }
    public void CoppyScriptableObj()
    {
        /*        isCoppy.NormalPosHolster = Source.NormalPosHolster;
                isCoppy.NormalRosHolster = Source.NormalRosHolster;
                isCoppy.NormalPosEquip = Source.NormalPosEquip;
                isCoppy.NormalRosEquip = Source.NormalRosEquip;*/
        //
        /*        isCoppy.ShortPosHolster = Source.ShortPosHolster;
                isCoppy.ShortRosHolster = Source.ShortRosHolster;
                isCoppy.ShortPosEquip = Source.ShortPosEquip;
                isCoppy.ShortRosEquip = Source.ShortRosEquip;
                //*/
        isCoppy.BigsizePosHolster = Source.BigsizePosHolster;
        isCoppy.BigsizeRosHolster = Source.BigsizeRosHolster;
        isCoppy.BigsizePosEquip = Source.BigsizePosEquip;
        isCoppy.BigsizeRosEquip = Source.BigsizeRosEquip;

    }

  
}
