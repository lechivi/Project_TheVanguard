using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_DroppableSlot : MonoBehaviour, IDropHandler
{
    private UI_DraggableItem child;

    private void Awake()
    {
        this.child = GetComponentInChildren<UI_DraggableItem>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        Debug.Log("OnDrop");
        UI_DraggableItem item = eventData.pointerDrag.GetComponent<UI_DraggableItem>();
        if (this.child == item) return;
        WeaponDataSO childWeaponData = this.child.GetWeaponData();

        this.child.SetWeaponData(item.GetWeaponData());
        this.child.SetModel();
        
        if (childWeaponData != null )
        {
            item.SetWeaponData(childWeaponData);
            item.SetModel();

        }
        else
        {
            item.ResetSlot();
        }

        UI_InventoryPanel.Instance.SetSelectEquippedSlot(this.child.UI_WeaponSlotParent);
        PlayerWeaponManager.Instance.SwitchWeapon(item.WeaponList, item.WeaponSlotIndex, this.child.WeaponList, this.child.WeaponSlotIndex);
    }
}
