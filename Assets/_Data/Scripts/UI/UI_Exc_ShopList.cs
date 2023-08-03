using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Exc_ShopList : SaiMonoBehaviour
{
    [Header("REFERENCE")]
    [SerializeField] private UI_ExchangePanel exchangePanel;
    [SerializeField] private Transform listPanel;
    [SerializeField] private List<UI_Exc_ItemSlot> shopSlotList = new List<UI_Exc_ItemSlot>();

    private NPCShopkeeperInteractable npc;

    public UI_ExchangePanel ExchangePanel { get => this.exchangePanel; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.exchangePanel == null)
            this.exchangePanel = transform.parent.GetComponent<UI_ExchangePanel>();

        if (this.listPanel == null)
            this.listPanel = transform.Find("Scroll").Find("ListPanel");

        if (this.shopSlotList.Count != this.listPanel.transform.GetComponentsInChildren<UI_Exc_ItemSlot>().Length)
            foreach (UI_Exc_ItemSlot slot in this.listPanel.transform.GetComponentsInChildren<UI_Exc_ItemSlot>())
            {
                if (this.shopSlotList.Contains(slot)) continue;
                this.shopSlotList.Add(slot);
            }
    }


    public void SetShopList(NPCShopkeeperInteractable npc)
    {
        foreach (var slot in this.shopSlotList)
        {
            slot.Hide();
        }

        if (npc.ItemList == null) return;
        this.npc = npc;

        for (int i = 0; i < this.npc.ItemList.Count; i++)
        {
            this.shopSlotList[i].Show();
            this.shopSlotList[i].Setup(this.npc.ItemList[i]);
        }
    }

    public void AddItemToShop(ItemDataSO itemData)
    {
        this.shopSlotList[this.npc.ItemList.Count].Show();
        this.shopSlotList[this.npc.ItemList.Count].Setup(itemData);
        this.npc.ItemList.Add(itemData);
    }

    public void RemoveItemFromShop(UI_Exc_ItemSlot itemSlot)
    {
        if (!this.shopSlotList.Contains(itemSlot)) return;

        this.npc.ItemList.Remove(itemSlot.ItemData);
        itemSlot.transform.SetAsLastSibling();
        itemSlot.Hide();
    }
}
