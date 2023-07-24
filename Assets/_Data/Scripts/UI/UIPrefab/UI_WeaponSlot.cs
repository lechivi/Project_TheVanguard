using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponSlot : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO weaponData;
    [SerializeField] protected Image selectedImage;
    [SerializeField] protected GameObject buttonList;
    [SerializeField] protected Button switchButton;
    [SerializeField] protected Button inspectButton;
    [SerializeField] protected Button dropButton;

    protected virtual void OnEnable()
    {
        //for overrite
    }

    public virtual void SetSelected(bool isSelected)
    {
        this.selectedImage.gameObject.SetActive(isSelected);
        this.buttonList.gameObject.SetActive(isSelected);
    }
}
