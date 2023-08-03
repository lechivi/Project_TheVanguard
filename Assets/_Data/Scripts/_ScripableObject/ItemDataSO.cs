using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataSO : ScriptableObject
{
    [Header("COMMON")]
    public string ItemName;
    public float ItemValue;
    public ItemType ItemType;
    public GameObject Icon; //UI
    public GameObject Model;
}
