using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class UI_DraggableItem : SaiMonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
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
        if (this.weaponData != null)
        {
            UI_WeaponInformation.Instance.SetInformation(this.weaponData);
            UI_WeaponInformation.Instance.SetCanvasGroupAlpha(1);

            UI_InventoryPanel.Instance.SetSelectEquippedSlot(this.ui_WeaponSlotParent);
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
        this.canvasGroup.blocksRaycasts = this.WeaponIconObject;
        this.canvasGroup.alpha = 1;

        this.rectTransform.localPosition = Vector3.zero;
    }

    public WeaponDataSO GetWeaponData()
    {
        return this.weaponData;
    }

    public void SetWeaponData(WeaponDataSO weaponData )
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

        this.canvasGroup.blocksRaycasts = false;
    }
}
