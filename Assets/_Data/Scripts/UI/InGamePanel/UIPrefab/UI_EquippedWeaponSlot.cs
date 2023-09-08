using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_EquippedWeaponSlot : UI_WeaponSlot
{
    [Header("REF_")]
    [SerializeField] private TMP_Text indexText;
    [SerializeField] private TMP_Text equippedText;
    [SerializeField] private Button equipButton;
    [SerializeField] private UI_Inv_EquippedList equippedList;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.equippedList == null)
            this.equippedList = transform.GetComponentInParent<UI_Inv_EquippedList>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        this.SetSelected(false);
        this.SetEquipped(false);
        this.indexText.SetText((transform.GetSiblingIndex() + 1).ToString());
    }

    public void SetEquipped(bool isEquipped)
    {
        if (GetComponentInChildren<UI_DraggableItem>().WeaponIconObject == null)
        {
            isEquipped = false;
        }
        this.equippedText.gameObject.SetActive(isEquipped);
    }

    #region BUTTON ClICK EVENT
    public void OnEquipButtonClicked()
    {
        this.equippedList.SetEquipSlot(transform.GetSiblingIndex());
    }

    public void OnSwitchButtobClicked()
    {

    }

    public void OnInspectButtonClicked()
    {

    }

    public void OnDropButtonClicked()
    {
        PlayerWeaponManager.Instance.RemoveWeaponFromEquipped(transform.GetSiblingIndex(), true);
        this.SetSelected(false);
        GetComponentInChildren<UI_DraggableItem>().ResetSlot();
    }
    #endregion
}
