using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Exc_ItemInfo : BaseUIElement
{
    [Header("ITEM INFO")]
    [SerializeField] private UI_ExchangePanel exchangePanel;
    [SerializeField] private TMP_Text itemValueText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform attributesContainer;
    [SerializeField] private List<UI_ItemAttribute> itemAttributeList;

    public UI_Exc_ItemSlot SelectedItemSlot;
    public UI_ExchangePanel ExchangePanel { get => this.exchangePanel; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.exchangePanel == null)
            this.exchangePanel = transform.parent.GetComponent<UI_ExchangePanel>();

        if (this.itemValueText == null)
            this.itemValueText = transform.Find("ValuePanel").Find("ValueText").GetComponent<TMP_Text>();

        if (this.iconContainer == null)
            this.iconContainer = transform.Find("IconContainer");

        if (this.attributesContainer == null)
            this.attributesContainer = transform.Find("AttributeList");

        if (this.itemAttributeList.Count != this.attributesContainer.GetComponentsInChildren<UI_ItemAttribute>().Length)
            foreach (var item in this.attributesContainer.GetComponentsInChildren<UI_ItemAttribute>())
            {
                if (this.itemAttributeList.Contains(item)) continue;
                this.itemAttributeList.Add(item);
            }
    }

    public void SetSelectItemSlot(UI_Exc_ItemSlot selectedSlot)
    {
        if (this.SelectedItemSlot == selectedSlot) return;

        if (this.SelectedItemSlot != null)
        {
            this.SelectedItemSlot.SetSelected(false);
        }

        this.SelectedItemSlot = selectedSlot;
        this.SelectedItemSlot.SetSelected(true);
    }

    public void SetDisplayItemAttributes(ItemDataSO itemData)
    {
        this.itemValueText.SetText(itemData.ItemValue.ToString());

        foreach (Transform child in this.iconContainer)
        {
            Destroy(child.gameObject);
        }
        if (itemData.Icon != null)
        {
            Instantiate(itemData.Icon, this.iconContainer);
        }

        foreach (var item in this.itemAttributeList)
        {
            item.Hide();
        }
        if (itemData.ItemType == ItemType.Weapon)
        {
            WeaponDataSO weaponData = itemData as WeaponDataSO;
            if (weaponData.WeaponType == WeaponType.Melee)
            {
                this.itemAttributeList[0].SetAttributeText("Damage", weaponData.MeleeDamage.ToString());
                this.itemAttributeList[0].Show(null);
                this.itemAttributeList[1].SetAttributeText("Swing speed", weaponData.SwingSpeed.ToString());
                this.itemAttributeList[1].Show(null);
            }
            else
            {
                this.itemAttributeList[0].SetAttributeText("Damage", weaponData.RangedDamage.ToString());
                this.itemAttributeList[0].Show(null);
                this.itemAttributeList[1].SetAttributeText("Fire rate", weaponData.FireRate.ToString());
                this.itemAttributeList[1].Show(null);
                this.itemAttributeList[2].SetAttributeText("Accuracy", weaponData.Accuracy.ToString());
                this.itemAttributeList[2].Show(null);
                this.itemAttributeList[3].SetAttributeText("Magazine size", weaponData.MagazineSize.ToString());
                this.itemAttributeList[3].Show(null);
                this.itemAttributeList[4].SetAttributeText("Range", weaponData.Range.ToString());
                this.itemAttributeList[4].Show(null);
            }
        }
        else if (itemData.ItemType == ItemType.Consumable)
        {
            ConsumableDataSO consumableData = itemData as ConsumableDataSO;

        }
        else if (itemData.ItemType == ItemType.Ammo)
        {
            AmmoDataSO ammoData = itemData as AmmoDataSO;

        }
    }
}
