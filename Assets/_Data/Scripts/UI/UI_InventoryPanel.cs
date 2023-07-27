using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventoryPanel : MonoBehaviour
{
    public static UI_InventoryPanel Instance;
    [SerializeField] private List<UI_DraggableItem> equippedListPanel = new List<UI_DraggableItem>();
    [SerializeField] private List<UI_DraggableItem> backpackListPanel = new List<UI_DraggableItem>();
    [SerializeField] private UI_EquippedListManager ui_EquippedListManager;

    public List<UI_DraggableItem> EquippedListPanel { get => this.equippedListPanel; }
    public List<UI_DraggableItem> BackpackListPanel { get => this.backpackListPanel; }
    public UI_EquippedListManager UI_EquippedListManager { get => this.ui_EquippedListManager; }

    public UI_WeaponSlot SelectedWeaponSlot;

    private void Awake()
    {
        UI_InventoryPanel.Instance = this;
    }

    public void ResetSlot()
    {
        foreach (UI_DraggableItem item in equippedListPanel)
        {
            if (item.WeaponIconObject != null) 
            {
                Destroy(item.WeaponIconObject.gameObject);
                item.WeaponIconObject = null;
            }
        }
        foreach (UI_DraggableItem item in backpackListPanel)
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
