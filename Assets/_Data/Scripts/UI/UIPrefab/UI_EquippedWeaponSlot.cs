using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_EquippedWeaponSlot : UI_WeaponSlot
{
    [SerializeField] private TMP_Text indexText;
    [SerializeField] private TMP_Text equippedText;
    [SerializeField] private Button equipButton;
    [SerializeField] UI_EquippedListManager equippedListManager;

    private void Awake()
    {
        this.equippedListManager.GetComponentInParent<UI_EquippedListManager>();
    }

    protected override void OnEnable()
    {
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
        this.equippedListManager.SetEquipSlot(transform.GetSiblingIndex());
    }

    public void OnSwitchButtobClicked()
    {

    }

    public void OnInspectButtonClicked()
    {

    }

    public void OnDropButtonClicked()
    {
        PlayerWeaponManager.Instance.RemoveWeaponFromEquipped(transform.GetSiblingIndex());
        this.SetSelected(false);
        GetComponentInChildren<UI_DraggableItem>().ResetSlot();
    }
    #endregion
}
