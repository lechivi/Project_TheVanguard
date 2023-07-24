using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BackpackWeaponSlot : UI_WeaponSlot
{
    protected override void OnEnable()
    {
        this.SetSelected(false);
    }

    #region BUTTON ClICK EVENT
    public void OnSwitchButtobClicked()
    {

    }

    public void OnInspectButtonClicked()
    {

    }

    public void OnDropButtonClicked()
    {
        PlayerWeaponManager.Instance.RemoveWeaponFromBackpack(transform.GetSiblingIndex());
        this.SetSelected(false);
        GetComponentInChildren<UI_DraggableItem>().ResetSlot();
    }
    #endregion
}
