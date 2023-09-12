using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponSlot : SaiMonoBehaviour
{
    [Header("REFERENCE")]
    [SerializeField] protected UI_DraggableItem draggableItem;
    [SerializeField] protected UI_DroppableSlot droppableSlot;
    [SerializeField] protected Button button;
    [SerializeField] protected Image selectedImage;
    [SerializeField] protected GameObject buttonList;
    [SerializeField] protected Button switchButton;
    [SerializeField] protected Button inspectButton;
    [SerializeField] protected Button dropButton;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.button == null)
            this.button = GetComponent<Button>();
    }

    protected virtual void OnEnable()
    {
        this.button.interactable = this.draggableItem.GetWeaponData() != null;
    }

    public virtual void SetSelected(bool isSelected)
    {
        this.selectedImage.gameObject.SetActive(isSelected);
        this.buttonList.gameObject.SetActive(isSelected);
    }

    public virtual void OnSlotButtonClicked()
    {
        Debug.Log("Clicked");
        this.draggableItem.OnPointerDownDraggable();
    }
}
