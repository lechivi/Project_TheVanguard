using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone_ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        UI_DraggableItem item = eventData.pointerDrag.GetComponent<UI_DraggableItem>();
        item.ResetSlot();
        item.UI_WeaponSlotParent.SetSelected(false);

        if (item.WeaponList == WeaponList.EquippedWeapons)
        {
            PlayerWeaponManager.Instance.RemoveWeaponFromEquipped(item.WeaponSlotIndex, true);
        }
        else
        {
            PlayerWeaponManager.Instance.RemoveWeaponFromBackpack(item.WeaponSlotIndex, true);
        }
    }
}
