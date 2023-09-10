using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_WeaponInfo : BaseUIElement
{
    [Header("WEAPON INFO")]
    [SerializeField] private List<Sprite> listWeaponIcon;
    [SerializeField] private Image iconImage;
    [SerializeField] private CanvasGroup textConatiner;
    [SerializeField] private TMP_Text curAmmoText;
    [SerializeField] private TMP_Text maxAmmoText;

    private WeaponType curWeaponType;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.iconImage == null)
            this.iconImage = transform.Find("Icon").GetComponent<Image>();

        if (this.textConatiner == null)
            this.textConatiner = transform.Find("TextContainer").GetComponent<CanvasGroup>();

        if (this.curAmmoText == null)
            this.curAmmoText = transform.Find("TextContainer/CurAmmo_Text").GetComponent<TMP_Text>();

        if (this.maxAmmoText == null)
            this.maxAmmoText = transform.Find("TextContainer/MaxAmmo_Text").GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        if (PlayerCtrl.HasInstance)
        {
            PlayerWeaponManager playerWeaponManager = PlayerCtrl.Instance.PlayerWeapon.PlayerWeaponManager;
            Weapon activeWeapon = playerWeaponManager.GetActiveWeapon();
            if (activeWeapon == null)
            {
                this.Hide();
                return;
            }
            else
            {
                this.Show(null);
                WeaponType weaponType = activeWeapon.WeaponData.WeaponType;
                if (weaponType == WeaponType.Melee)
                {
                    this.SetWeapon(weaponType);
                }
                else
                {
                    int curAmmo = playerWeaponManager.GetActiveRaycastWeapon().currentAmmo;
                    int maxAmmo = activeWeapon.WeaponData.MagazineSize;
                    this.SetWeapon(weaponType, curAmmo, maxAmmo);
                }
            }
        }
    }
    public void SetWeapon(WeaponType weaponType, int? curAmmo = 0, int? maxAmmo = 0)
    {
        if (this.listWeaponIcon.Count != 5) return;

        if (this.curWeaponType != weaponType)
        {
            this.curWeaponType = weaponType;
            switch (weaponType)
            {
                case WeaponType.Pistol:
                    this.iconImage.sprite = this.listWeaponIcon[0];
                    break;
                case WeaponType.AssaultRifle:
                    this.iconImage.sprite = this.listWeaponIcon[1];
                    break;
                case WeaponType.Shotgun:
                    this.iconImage.sprite = this.listWeaponIcon[2];
                    break;
                case WeaponType.SniperRifle:
                    this.iconImage.sprite = this.listWeaponIcon[3];
                    break;
                case WeaponType.Melee:
                    this.iconImage.sprite = this.listWeaponIcon[4];
                    break;
            }
        }

        if (weaponType == WeaponType.Melee)
        {
            this.textConatiner.alpha = 0;
        }
        else
        {
            this.textConatiner.alpha = 1;
            this.curAmmoText.SetText(curAmmo.ToString());
            this.maxAmmoText.SetText(maxAmmo.ToString());
        }
    }
}
