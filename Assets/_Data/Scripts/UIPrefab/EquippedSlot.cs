using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquippedSlot : MonoBehaviour
{
    [SerializeField] private WeaponDataSO weaponData;
    [SerializeField] private Image selectedImage;
    [SerializeField] private TMP_Text indexText;
    [SerializeField] private TMP_Text equippedText;
    [SerializeField] private GameObject buttonList;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button switchButton;
    [SerializeField] private Button inspectButton;
    [SerializeField] private Button dropButton;

    private bool isSelected;
    private bool isEquipped;

    private void Awake()
    {
        this.SetSelected(true);
    }

    public void SetSelected(bool isSelected)
    {
        this.selectedImage.gameObject.SetActive(isSelected);
        this.buttonList.gameObject.SetActive(isSelected);

        //Check is equipped
        this.equippedText.gameObject.SetActive(this.isEquipped);
    }

    public void SetIndex(int index)
    {
        this.indexText.SetText(index.ToString());
    }

    public void OnEquipButtonClicked()
    {
        this.isEquipped = !this.isEquipped;
        this.equippedText.gameObject.SetActive(this.isEquipped);
    }

    public void OnSwitchButtobClicked()
    {

    }

    public void OnInspectButtonClicked()
    {

    }

    public void OnDropButtonClicked()
    {

    }
}
