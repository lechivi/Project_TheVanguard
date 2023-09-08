using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inv_WeaponInfo : BaseUIElement
{
    public static UI_Inv_WeaponInfo Instance;

    [Header("WEAPON INFO")]
    [SerializeField] private TMP_Text weaponNameText;
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private List<UI_ItemAttribute> itemAttributeList;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.weaponNameText == null)
            this.weaponNameText = transform.Find("WeaponName_Text").GetComponent<TMP_Text>();

        if (this.valueText == null)
            this.valueText = transform.Find("ValuePanel").GetComponentInChildren<TMP_Text>();

        if (this.itemAttributeList.Count != transform.Find("Detail").GetComponentsInChildren<UI_ItemAttribute>().Length)
            foreach (var item in transform.Find("Detail").GetComponentsInChildren<UI_ItemAttribute>())
            {
                if (this.itemAttributeList.Contains(item)) continue;
                this.itemAttributeList.Add(item);
            }
    }


    protected override void Awake()
    {
        base.Awake();
        UI_Inv_WeaponInfo.Instance = this;

        this.Hide();
    }

    public void SetInformation(WeaponDataSO weaponData)
    {
        this.weaponNameText.SetText(weaponData.ItemName);
        this.valueText.SetText(weaponData.ItemValue.ToString());

        foreach (var item in this.itemAttributeList)
        {
            item.Hide();
        }

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

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

    }
}
