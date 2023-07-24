using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_WeaponInformation : MonoBehaviour
{
    public static UI_WeaponInformation Instance;
    [SerializeField] private TMP_Text weaponNameText;
    [SerializeField] private Transform rangedDamage;
    [SerializeField] private Transform fireRate;
    [SerializeField] private Transform accuracy;
    [SerializeField] private Transform magazineSize;
    [SerializeField] private Transform reloadTime;
    [SerializeField] private Transform meleeDamage;
    [SerializeField] private Transform swingSpeed;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        UI_WeaponInformation.Instance = this;
        this.weaponNameText = transform.Find("NameText").GetComponent<TMP_Text>();
        Transform detaiTransform = transform.Find("Detail");
        this.rangedDamage = detaiTransform.Find("Damage");
        this.fireRate = detaiTransform.Find("FireRate");
        this.accuracy = detaiTransform.Find("Accuracy");
        this.magazineSize = detaiTransform.Find("MagazineSize");
        this.reloadTime = detaiTransform.Find("ReloadTime");
        this.meleeDamage = detaiTransform.Find("MeleeDamage");
        this.swingSpeed = detaiTransform.Find("SwingSpeed");
        this.canvasGroup = GetComponent<CanvasGroup>();

        this.canvasGroup.alpha = 0;
    }

    public void SetInformation(WeaponDataSO weaponData)
    {
        if (weaponData.WeaponType != WeaponType.Melee)
        {
            this.rangedDamage.gameObject.SetActive(true);
            this.fireRate.gameObject.SetActive(true);
            this.accuracy.gameObject.SetActive(true);
            this.magazineSize.gameObject.SetActive(true);
            this.reloadTime.gameObject.SetActive(true);
            this.meleeDamage.gameObject.SetActive(false);
            this.swingSpeed.gameObject.SetActive(false);

            this.weaponNameText.SetText(weaponData.WeaponName);
            this.rangedDamage.Find("Value").GetComponent<TMP_Text>().SetText(weaponData.RangedDamage.ToString());
            this.fireRate.Find("Value").GetComponent<TMP_Text>().SetText(weaponData.FireRate.ToString());
            this.accuracy.Find("Value").GetComponent<TMP_Text>().SetText(weaponData.Accuracy.ToString());
            this.magazineSize.Find("Value").GetComponent<TMP_Text>().SetText(weaponData.MagazineSize.ToString());
            this.reloadTime.Find("Value").GetComponent<TMP_Text>().SetText(weaponData.ReloadTime.ToString());
        }
        else
        {
            this.rangedDamage.gameObject.SetActive(false);
            this.fireRate.gameObject.SetActive(false);
            this.accuracy.gameObject.SetActive(false);
            this.magazineSize.gameObject.SetActive(false);
            this.reloadTime.gameObject.SetActive(false);
            this.meleeDamage.gameObject.SetActive(true);
            this.swingSpeed.gameObject.SetActive(true);

            this.weaponNameText.SetText(weaponData.WeaponName);
            this.meleeDamage.Find("Value").GetComponent<TMP_Text>().SetText(weaponData.MeleeDamage.ToString());
            this.swingSpeed.Find("Value").GetComponent<TMP_Text>().SetText(weaponData.SwingSpeed.ToString());
        }
    }

    public void SetCanvasGroupAlpha(int alpha)
    {
        this.canvasGroup.alpha = alpha;
    }
}
