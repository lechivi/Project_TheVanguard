using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopSlot : MonoBehaviour
{
    [SerializeField] private Image selectImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text text;
    //Pistol,
    //AssaultRifle,
    //Shotgun,
    //SniperRifle,
    //Melee,
    [Space(10)]
    [SerializeField] private WeaponDataSO weaponData;
    [SerializeField] private List<Sprite> icons = new List<Sprite>();

    private void OnEnable()
    {
        this.Setup();
    }

    private void Setup()
    {
        if (this.weaponData == null) return;

        this.selectImage.gameObject.SetActive(false);
        this.iconImage.sprite = this.icons[(int)this.weaponData.WeaponType - 1];
        this.text.SetText(this.weaponData.WeaponName);

    }
}
