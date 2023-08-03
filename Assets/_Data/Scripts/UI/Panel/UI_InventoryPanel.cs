using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventoryPanel : SaiMonoBehaviour
{
    public static UI_InventoryPanel Instance;

    [Header("REFERENCE")]
    [SerializeField] private UI_Inv_EquippedList equippedList;
    [SerializeField] private List<UI_DraggableItem> draggablesEquippedList = new List<UI_DraggableItem>();
    [SerializeField] private List<UI_DraggableItem> draggablesBackpackList = new List<UI_DraggableItem>();

    public UI_WeaponSlot SelectedWeaponSlot;

    public List<UI_DraggableItem> DraggablesEquippedList { get => this.draggablesEquippedList; }
    public List<UI_DraggableItem> DraggablesBackpackList { get => this.draggablesBackpackList; }
    public UI_Inv_EquippedList EquippedList { get => this.equippedList; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.equippedList == null)
            this.equippedList = GetComponentInChildren<UI_Inv_EquippedList>();

        if (this.draggablesEquippedList.Count != transform.Find("Inv_EquippedList").GetComponentsInChildren<UI_DraggableItem>().Length)
            foreach (var item in transform.Find("Inv_EquippedList").GetComponentsInChildren<UI_DraggableItem>())
            {
                this.draggablesEquippedList.Add(item);
            }

        if (this.draggablesBackpackList.Count != transform.Find("Inv_BackpackList").GetComponentsInChildren<UI_DraggableItem>().Length)
            foreach (var item in transform.Find("Inv_BackpackList").GetComponentsInChildren<UI_DraggableItem>())
            {
                this.draggablesBackpackList.Add(item);
            }
    }


    protected override void Awake()
    {
        base.Awake();
        UI_InventoryPanel.Instance = this;
    }

    public void ResetSlot()
    {
        foreach (UI_DraggableItem item in draggablesEquippedList)
        {
            if (item.WeaponIconObject != null)
            {
                Destroy(item.WeaponIconObject.gameObject);
                item.WeaponIconObject = null;
            }
        }
        foreach (UI_DraggableItem item in draggablesBackpackList)
        {
            if (item.WeaponIconObject != null)
            {
                Destroy(item.WeaponIconObject.gameObject);
                item.WeaponIconObject = null;
            }
        }
    }

    public void SetSelectEquippedSlot(UI_WeaponSlot selectedSlot)
    {
        if (this.SelectedWeaponSlot != null)
        {
            this.SelectedWeaponSlot.SetSelected(false);
        }

        this.SelectedWeaponSlot = selectedSlot;
        this.SelectedWeaponSlot.SetSelected(true);
    }
}
