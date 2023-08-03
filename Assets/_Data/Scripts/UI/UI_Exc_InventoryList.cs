using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Exc_InventoryList : SaiMonoBehaviour
{
    [Header("REFERENCE")]
    [SerializeField] private UI_ExchangePanel exchangePanel;
    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private TMP_Text transactionText;
    [SerializeField] private Transform equippedListPanel;
    [SerializeField] private Transform backpackListPanel;
    [SerializeField] private List<UI_Exc_ItemSlot> equippedSlotList = new List<UI_Exc_ItemSlot>();
    [SerializeField] private List<UI_Exc_ItemSlot> backpackSlotList = new List<UI_Exc_ItemSlot>();

    [SerializeField] private List<RectTransform> rebuidLayoutRectTransforms = new List<RectTransform>();
    public UI_ExchangePanel ExchangePanel { get => this.exchangePanel; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.exchangePanel == null)
            this.exchangePanel = transform.parent.GetComponent<UI_ExchangePanel>();

        if (this.currencyText == null)
            this.currencyText = transform.Find("CurrencyPanel").Find("CurrencyText").GetComponent<TMP_Text>();

        if (this.transactionText == null)
            this.transactionText = transform.Find("CurrencyPanel").Find("TransactionText").GetComponent<TMP_Text>();

        if (this.equippedListPanel == null)
            this.equippedListPanel = transform.Find("Scroll").Find("ListPanel").Find("EquippedListPanel");

        if (this.backpackListPanel == null)
            this.backpackListPanel = transform.Find("Scroll").Find("ListPanel").Find("BackpackListPanel");

        if (this.equippedListPanel != null && this.equippedSlotList.Count != this.equippedListPanel.transform.GetComponentsInChildren<UI_Exc_ItemSlot>().Length)
            foreach (UI_Exc_ItemSlot slot in this.equippedListPanel.transform.GetComponentsInChildren<UI_Exc_ItemSlot>())
            {
                if (this.equippedSlotList.Contains(slot)) continue;
                this.equippedSlotList.Add(slot);
            }

        if (this.backpackListPanel != null && this.backpackSlotList.Count != this.backpackListPanel.transform.GetComponentsInChildren<UI_Exc_ItemSlot>().Length)
            foreach (UI_Exc_ItemSlot slot in this.backpackListPanel.transform.GetComponentsInChildren<UI_Exc_ItemSlot>())
            {
                if (this.backpackSlotList.Contains(slot)) continue;
                this.backpackSlotList.Add(slot);
            }

        //TODO: just example
        this.currencyText.SetText(this.exchangePanel.PlayerCurrency.ToString());
    }


    public void SetInventoryList()
    {
        foreach (var slot in this.equippedSlotList)
        {
            slot.Hide();
        }
        foreach (var slot in this.backpackSlotList)
        {
            slot.Hide();
        }

        List<Weapon> equippedWeapons = PlayerWeaponManager.Instance.EquippedWeapons.GetList();
        this.equippedListPanel.gameObject.SetActive(!PlayerWeaponManager.Instance.EquippedWeapons.IsAllNull());
        for (int i = 0; i < equippedWeapons.Count; i++)
        {
            if (equippedWeapons[i] == null)
            {
                this.equippedSlotList[i].Hide();
            }
            else
            {
                this.equippedSlotList[i].Setup(equippedWeapons[i].WeaponData);
                this.equippedSlotList[i].Show();
            }
        }

        List<Weapon> backpackWeapons = PlayerWeaponManager.Instance.BackpackWeapons.GetList();
        this.backpackListPanel.gameObject.SetActive(!PlayerWeaponManager.Instance.BackpackWeapons.IsAllNull());
        for (int i = 0; i < backpackWeapons.Count; i++)
        {
            if (backpackWeapons[i] == null)
            {
                this.backpackSlotList[i].Hide();
            }
            else
            {
                this.backpackSlotList[i].Setup(backpackWeapons[i].WeaponData);
                this.backpackSlotList[i].Show();
            }
        }

        for (int i = 0; i < this.rebuidLayoutRectTransforms.Count; i++)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(this.rebuidLayoutRectTransforms[i]);
        }
    }

    public void SetTransactionText(float amount, bool isBuy)
    {
        if (isBuy)
        {
            this.transactionText.SetText("-" + amount);
            this.transactionText.color = Color.red;
        }
        else
        {
            this.transactionText.SetText("+" + amount);
            this.transactionText.color = Color.green;
        }
    }

    public void SetCurrencyText(float amount)
    {
        this.currencyText.SetText(amount.ToString());
    }

    public int CheckListContain(UI_Exc_ItemSlot itemSlot)
    {
        if (this.equippedSlotList.Contains(itemSlot))
            return 1;

        if (this.backpackSlotList.Contains(itemSlot))
            return 2;

        return 0;
    }
}
