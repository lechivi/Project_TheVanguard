using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_EquippedListManager : SaiMonoBehaviour
{
    [SerializeField] private UI_EquippedWeaponSlot equippedSlotPrefab;
    [SerializeField] private List<UI_EquippedWeaponSlot> equippedSlots = new List<UI_EquippedWeaponSlot>();
    public List<UI_EquippedWeaponSlot> EquippedSlots { get => this.equippedSlots; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadEquippedSlots();
    }
    protected virtual void LoadEquippedSlots()
    {
        if (transform.GetComponentsInChildren<UI_EquippedWeaponSlot>().ToList().Count == this.equippedSlots.Count) return;
        this.equippedSlots = transform.GetComponentsInChildren<UI_EquippedWeaponSlot>().ToList();
        Debug.LogWarning(transform.name + ": LoadEquippedSlots", gameObject);
    }

    public void SetNumberElementOfList(int number)
    {
        if (this.equippedSlotPrefab == null) return;

        int addAmount = number - this.equippedSlots.Count;
        if (addAmount <= 0) return;

        for (int i = 0; i < addAmount; i++)
        {
            UI_EquippedWeaponSlot newSlot = Instantiate(this.equippedSlotPrefab);
            this.equippedSlots.Add(newSlot);
        }
    }

    //TODO: change to both weapon list
    public void SetSelectSlot(int index)
    {
        if (index > this.equippedSlots.Count - 1) return;
        for (int i = 0; i < this.equippedSlots.Count; i++)
        {
            if (i == index)
            {
                this.equippedSlots[i].SetSelected(true);
            }
            else
            {
                this.equippedSlots[i].SetSelected(false);
            }
        }
    }

    //TODO: connect with PlayerWeaponManager
    public void SetEquipSlot(int index)
    {
        if (index > this.equippedSlots.Count - 1) return;
        for (int i = 0; i < this.equippedSlots.Count; i++)
        {
            if (i == index)
            {
                this.equippedSlots[i].SetEquipped(true);
            }
            else
            {
                this.equippedSlots[i].SetEquipped(false);
            }
        }
    }
}
