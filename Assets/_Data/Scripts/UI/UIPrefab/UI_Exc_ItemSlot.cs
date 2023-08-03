using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ItemSlotType { InventorySlot, ShopSlot }

public class UI_Exc_ItemSlot : SaiMonoBehaviour, IPointerDownHandler
{
    [Header("REFERENCE")]
    [SerializeField] private Image selectImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text text;
    [SerializeField] private LayoutElement layoutElement;

    [Space(10)]
    [SerializeField] private List<Sprite> weaponIconList = new List<Sprite>();

    public ItemDataSO ItemData;
    public ItemSlotType ItemSlotType;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.layoutElement == null)
            this.layoutElement = GetComponent<LayoutElement>();
    }


    public void Show()
    {
        gameObject.SetActive(true);
        this.layoutElement.ignoreLayout = false;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        this.layoutElement.ignoreLayout = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UI_ExchangePanel exchangePanel = GetComponentInParent<UI_ExchangePanel>();
        if (exchangePanel == null) return;

        exchangePanel.OnItemSlotClicked(this);
    }

    public void Setup(ItemDataSO itemData)
    {
        this.ItemData = itemData;

        this.selectImage.gameObject.SetActive(false);
        this.text.SetText(this.ItemData.ItemName);
        if (this.ItemData.ItemType == ItemType.Weapon)
        {
            WeaponDataSO weaponData = itemData as WeaponDataSO;
            this.iconImage.sprite = this.weaponIconList[(int)weaponData.WeaponType - 1];
        }

    }

    public void SetSelected(bool isSelected)
    {
        this.selectImage.gameObject.SetActive(isSelected);
        this.text.fontStyle = isSelected ? FontStyles.Bold : FontStyles.Normal;
    }
}
