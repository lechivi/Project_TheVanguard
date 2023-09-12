using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UI_DraggableItem : SaiMonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("REFERENCE")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera canvasCamera;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private UI_WeaponSlot ui_WeaponSlotParent;

    [SerializeField] private WeaponDataSO weaponData;

    public UI_WeaponSlot UI_WeaponSlotParent { get => this.ui_WeaponSlotParent; }
    public int WeaponSlotIndex;
    public WeaponList WeaponList;
    public GameObject WeaponIconObject;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCanvas();
        this.LoadCanvasCamera();
        this.LoadRectTransform();
        this.LoadCanvasGroup();
        this.LoadUIWeaponSlot();
    }
    protected virtual void LoadCanvas()
    {
        if (this.canvas != null) return;
        this.canvas = GetComponentInParent<Canvas>();
        Debug.LogWarning(gameObject.name + ": LoadCanvas", gameObject);
    }
    protected virtual void LoadCanvasCamera()
    {
        if (this.canvasCamera != null) return;
        this.canvasCamera = this.canvas.worldCamera;
        Debug.LogWarning(gameObject.name + ": LoadCanvasCamera", gameObject);
    }
    protected virtual void LoadRectTransform()
    {
        if (this.rectTransform == null)
        {
            this.rectTransform = GetComponent<RectTransform>();
            Debug.LogWarning(gameObject.name + ": LoadRectTransform", gameObject);

        }
    }
    protected virtual void LoadCanvasGroup()
    {
        if (this.canvasGroup == null)
        {
            this.canvasGroup = GetComponent<CanvasGroup>();
            Debug.LogWarning(gameObject.name + ": LoadCanvasGroup", gameObject);

        }
    }
    protected virtual void LoadUIWeaponSlot()
    {
        if (this.ui_WeaponSlotParent != null) return;
        this.ui_WeaponSlotParent = GetComponentInParent<UI_WeaponSlot>();
        Debug.LogWarning(transform.name + ": LoadUIWeaponSlot", gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.OnPointerDownDraggable();
    }

    public void OnPointerDownDraggable()
    {
        if (this.weaponData != null)
        {
            if (UIManager.HasInstance)
            {
                UI_InventoryPanel inventoryPanel = UIManager.Instance.InGamePanel.PauseMenu.InventoryPanel;
                inventoryPanel.WeaponInfo.Show(null);
                inventoryPanel.WeaponInfo.SetInformation(this.weaponData);
                inventoryPanel.SetSelectEquippedSlot(this.ui_WeaponSlotParent);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.canvasGroup.blocksRaycasts = false;
        this.canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition += eventData.delta / this.canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.canvasGroup.blocksRaycasts = true;
        this.canvasGroup.alpha = 1;

        this.rectTransform.localPosition = Vector3.zero;
    }

    public WeaponDataSO GetWeaponData()
    {
        return this.weaponData;
    }

    public void SetActiveSlot(bool isActive)
    {
        Button button = this.UI_WeaponSlotParent.GetComponent<Button>();
        if (button != null)
        {
            button.interactable = isActive;
        }
    }

    public void SetWeaponData(WeaponDataSO weaponData)
    {
        this.weaponData = weaponData;
    }

    public void SetModel()
    {
        if (this.weaponData == null) return;
        if (WeaponIconObject != null)
        {
            Destroy(WeaponIconObject);
        }
        this.WeaponIconObject = Instantiate(this.weaponData.Icon, transform);

        this.canvasGroup.blocksRaycasts = true;
    }

    public void ResetSlot()
    {
        Debug.Log("ResetSlot");
        this.weaponData = null;
        Destroy(this.WeaponIconObject);
        this.WeaponIconObject = null;

        //this.canvasGroup.blocksRaycasts = true;
        //this.ui_WeaponSlotParent.SetSelected(false);
    }
}
