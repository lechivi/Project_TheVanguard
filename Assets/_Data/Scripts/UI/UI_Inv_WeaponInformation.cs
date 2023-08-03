using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inv_WeaponInformation : SaiMonoBehaviour
{
    public static UI_Inv_WeaponInformation Instance;

    [Header("REFERENCE")]
    [SerializeField] private TMP_Text weaponNameText;
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private List<UI_ItemAttribute> itemAttributeList;

    [SerializeField] private CanvasGroup canvasGroup;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.weaponNameText == null)
            this.weaponNameText = transform.Find("NameText").GetComponent<TMP_Text>();

        if (this.valueText == null)
            this.valueText = transform.Find("ValuePanel").GetComponentInChildren<TMP_Text>();

        if (this.itemAttributeList.Count != transform.Find("Detail").GetComponentsInChildren<UI_ItemAttribute>().Length)
            foreach (var item in transform.Find("Detail").GetComponentsInChildren<UI_ItemAttribute>())
            {
                if (this.itemAttributeList.Contains(item)) continue;
                this.itemAttributeList.Add(item);
            }

        if (this.canvasGroup == null)
            this.canvasGroup = GetComponent<CanvasGroup>();
    }


    protected override void Awake()
    {
        base.Awake();
        UI_Inv_WeaponInformation.Instance = this;

        this.Hide();
    }

    public void Show()
    {
        this.canvasGroup.alpha = 1;
    }

    public void Hide()
    {
        this.canvasGroup.alpha = 0;
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
            this.itemAttributeList[0].Show();
            this.itemAttributeList[1].SetAttributeText("Swing speed", weaponData.SwingSpeed.ToString());
            this.itemAttributeList[1].Show();
        }
        else
        {
            this.itemAttributeList[0].SetAttributeText("Damage", weaponData.RangedDamage.ToString());
            this.itemAttributeList[0].Show();
            this.itemAttributeList[1].SetAttributeText("Fire rate", weaponData.FireRate.ToString());
            this.itemAttributeList[1].Show();
            this.itemAttributeList[2].SetAttributeText("Accuracy", weaponData.Accuracy.ToString());
            this.itemAttributeList[2].Show();
            this.itemAttributeList[3].SetAttributeText("Magazine size", weaponData.MagazineSize.ToString());
            this.itemAttributeList[3].Show();
            this.itemAttributeList[4].SetAttributeText("Reload Time", weaponData.ReloadTime.ToString());
            this.itemAttributeList[4].Show();
            this.itemAttributeList[5].SetAttributeText("Range", weaponData.Range.ToString());
            this.itemAttributeList[5].Show();
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

    }
}
