using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ExchangePanel : SaiMonoBehaviour
{
    [Header("REFERENCE")]
    [SerializeField] private UI_Exc_ShopList shopList;
    [SerializeField] private UI_Exc_InventoryList inventoryList;
    [SerializeField] private UI_Exc_ItemInformation itemInformation;

    [SerializeField] private CanvasGroup canvasGroup;

    [Space(10)]
    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;

    public float PlayerCurrency = 1000;
    public UI_Exc_ShopList ShopList { get => this.shopList; }
    public UI_Exc_InventoryList InventoryList { get => this.inventoryList; }
    public UI_Exc_ItemInformation ItemInformation { get => this.itemInformation; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.shopList == null)
            this.shopList = GetComponentInChildren<UI_Exc_ShopList>();

        if (this.inventoryList == null)
            this.inventoryList = GetComponentInChildren<UI_Exc_InventoryList>();

        if (this.itemInformation == null)
            this.itemInformation = GetComponentInChildren<UI_Exc_ItemInformation>();

        if (this.canvasGroup == null)
            this.canvasGroup = GetComponent<CanvasGroup>();
    }


    protected override void Awake()
    {
        base.Awake();
        this.Hide();
    }

    private void OnEnable()
    {
        this.itemInformation.Hide();
        this.buyButton.gameObject.SetActive(false);
        this.sellButton.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.canvasGroup.alpha = 1f;
        this.canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        this.canvasGroup.alpha = 0f;
        this.canvasGroup.blocksRaycasts = false;
    }

    public void OnItemSlotClicked(UI_Exc_ItemSlot itemSlot)
    {
        if (itemSlot == this.itemInformation.SelectedItemSlot) return;

        this.ItemInformation.Show();
        this.ItemInformation.SetSelectItemSlot(itemSlot);
        this.itemInformation.SetDisplayItemAttributes(itemSlot.ItemData);
        this.inventoryList.SetTransactionText(itemSlot.ItemData.ItemValue, itemSlot.ItemSlotType == ItemSlotType.ShopSlot);

        this.buyButton.gameObject.SetActive(itemSlot.ItemSlotType == ItemSlotType.ShopSlot);
        this.sellButton.gameObject.SetActive(itemSlot.ItemSlotType == ItemSlotType.InventorySlot);
    }

    public void OnBuyButtonClicked()
    {
        UI_Exc_ItemSlot soldItemSlot = this.itemInformation.SelectedItemSlot;
        if (soldItemSlot == null) return;

        Weapon boughtWeapon = soldItemSlot.ItemData.Model.GetComponent<Weapon>();
        if (boughtWeapon == null)
        {
            Debug.Log("Can't get Weapon");
            return;
        }
        if (soldItemSlot.ItemData.ItemValue > this.PlayerCurrency)
        {
            Debug.Log("Don't have enought currency");
            return;
        }
        if (PlayerWeaponManager.Instance.AddWeapon(boughtWeapon))
        {
            Debug.Log("Player bought a weapon");

            //Calculate player currency after buying
            this.PlayerCurrency -= soldItemSlot.ItemData.ItemValue;
            this.inventoryList.SetCurrencyText(this.PlayerCurrency);

            //Remove the bought item from shop
            this.shopList.RemoveItemFromShop(soldItemSlot);

            this.ItemInformation.Hide();
            this.inventoryList.SetInventoryList();
        }
    }

    public void OnSellButtonClicked()
    {
        UI_Exc_ItemSlot soldItemSlot = this.itemInformation.SelectedItemSlot;
        if (soldItemSlot == null) return;

        int index = this.inventoryList.CheckListContain(soldItemSlot);
        switch (index)
        {
            case 1:
                PlayerWeaponManager.Instance.RemoveWeaponFromEquipped(soldItemSlot.transform.GetSiblingIndex(), false);
                Debug.Log("Player sold a weapon from Equipped");
                break;
            case 2:
                PlayerWeaponManager.Instance.RemoveWeaponFromBackpack(soldItemSlot.transform.GetSiblingIndex(), false);
                Debug.Log("Player sold a weapon from Backpack");
                break;
            default:
                Debug.Log("Can't find that weapon in inventory");
                break;
        }

        //Calculate player currency after buying
        this.PlayerCurrency += soldItemSlot.ItemData.ItemValue;
        this.inventoryList.SetCurrencyText(this.PlayerCurrency);

        //Add the sold item to shop
        this.shopList.AddItemToShop(soldItemSlot.ItemData);

        this.ItemInformation.Hide();
        this.inventoryList.SetInventoryList();
    }
}
