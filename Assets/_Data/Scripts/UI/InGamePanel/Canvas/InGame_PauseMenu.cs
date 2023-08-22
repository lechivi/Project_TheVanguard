using UnityEngine;

public class InGame_PauseMenu : BaseUIElement
{
    [Header("CANVAS_PAUSE MENU")]
    [SerializeField] private UI_InventoryPanel ui_InventoryPanel;

    public UI_InventoryPanel UI_InventoryPanel { get => ui_InventoryPanel; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.ui_InventoryPanel == null)
            this.ui_InventoryPanel = GetComponentInChildren<UI_InventoryPanel>();
    }
}
