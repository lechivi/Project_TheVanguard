using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_WeaponInfo : BaseUIElement
{
    [Header("WEAPON INFO")]
    [SerializeField] private TMP_Text curAmmoText;
    [SerializeField] private TMP_Text maxAmmoText;
    [SerializeField] private Image iconImage;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.curAmmoText == null )
            this.curAmmoText = transform.Find("TextContainer/CurAmmo_Text").GetComponent<TMP_Text>();  
        
        if (this.maxAmmoText == null )
            this.maxAmmoText = transform.Find("TextContainer/MaxAmmo_Text").GetComponent<TMP_Text>();

        if (this.iconImage == null )
            this.iconImage = transform.Find("Icon").GetComponent<Image>();
    }
}
