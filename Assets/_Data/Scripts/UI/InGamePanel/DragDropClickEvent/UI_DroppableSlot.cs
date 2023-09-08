using UnityEngine;
using UnityEngine.EventSystems;

public class UI_DroppableSlot : SaiMonoBehaviour, IDropHandler
{
    [Header("REFERENCE")]
    [SerializeField] private UI_DraggableItem childDraggable;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.childDraggable == null)
            this.childDraggable = GetComponentInChildren<UI_DraggableItem>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        UI_DraggableItem item = eventData.pointerDrag.GetComponent<UI_DraggableItem>();
        if (this.childDraggable == item) return;
        WeaponDataSO childWeaponData = this.childDraggable.GetWeaponData();

        this.childDraggable.SetWeaponData(item.GetWeaponData());
        this.childDraggable.SetModel();
        this.childDraggable.SetActiveSlot(true);

        if (childWeaponData != null)
        {
            item.SetWeaponData(childWeaponData);
            item.SetModel();
            item.SetActiveSlot(true);
        }
        else
        {
            item.SetActiveSlot(false);
            item.ResetSlot();
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.InGamePanel.PauseMenu.InventoryPanel.SetSelectEquippedSlot(this.childDraggable.UI_WeaponSlotParent);
        }

        PlayerWeaponManager.Instance.SwitchWeapon(item.WeaponList, item.WeaponSlotIndex, this.childDraggable.WeaponList, this.childDraggable.WeaponSlotIndex);
    }
}
