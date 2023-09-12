using System.Collections.Generic;
using UnityEngine;

public class UI_InventoryPanel : BaseUIElement
{
    [Header("INVENTORY PANEL")]
    [SerializeField] private UiAppear uiAppear;
    [SerializeField] private UI_Inv_EquippedList equippedList;
    [SerializeField] private List<UI_DraggableItem> draggablesEquippedList = new List<UI_DraggableItem>();
    [SerializeField] private List<UI_DraggableItem> draggablesBackpackList = new List<UI_DraggableItem>();
    [SerializeField] private UI_Inv_WeaponInfo weaponInfo;

    public UI_WeaponSlot SelectedWeaponSlot;

    public UiAppear UiAppear { get => this.uiAppear; }
    public UI_Inv_EquippedList EquippedList { get => this.equippedList; }
    public List<UI_DraggableItem> DraggablesEquippedList { get => this.draggablesEquippedList; }
    public List<UI_DraggableItem> DraggablesBackpackList { get => this.draggablesBackpackList; }
    public UI_Inv_WeaponInfo WeaponInfo { get => this.weaponInfo; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.uiAppear == null)
            this.uiAppear = GetComponentInChildren<UiAppear>();

        if (this.equippedList == null)
            this.equippedList = GetComponentInChildren<UI_Inv_EquippedList>();

        if (this.draggablesEquippedList.Count != transform.Find("Container/Inv_EquippedList").GetComponentsInChildren<UI_DraggableItem>().Length)
            foreach (var item in transform.Find("Container/Inv_EquippedList").GetComponentsInChildren<UI_DraggableItem>())
            {
                this.draggablesEquippedList.Add(item);
            }

        if (this.draggablesBackpackList.Count != transform.Find("Container/Inv_BackpackList").GetComponentsInChildren<UI_DraggableItem>().Length)
            foreach (var item in transform.Find("Container/Inv_BackpackList").GetComponentsInChildren<UI_DraggableItem>())
            {
                this.draggablesBackpackList.Add(item);
            }

        if (this.weaponInfo == null)
            this.weaponInfo = GetComponentInChildren<UI_Inv_WeaponInfo>();
    }

    public void ResetSlot()
    {
        foreach (UI_DraggableItem item in this.draggablesEquippedList)
        {
            if (item.WeaponIconObject != null)
            {
                Destroy(item.WeaponIconObject.gameObject);
                item.WeaponIconObject = null;
            }
        }
        foreach (UI_DraggableItem item in this.draggablesBackpackList)
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
