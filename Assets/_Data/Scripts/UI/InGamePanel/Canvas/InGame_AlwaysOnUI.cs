using UnityEngine;
using UnityEngine.UI;

public class InGame_AlwaysOnUI : BaseUIElement
{
    [Header("CANVAS_ALWAYS ON UI")]
    [SerializeField] private Image crosshair_Image;
    [SerializeField] private UI_PlayerInteract ui_PlayerInteract;
    [SerializeField] private UI_PlayerInfoScanner ui_PlayerInfoScanner;
    [SerializeField] private UI_Skill ui_Skill;

    public Image Crosshair_Image { get => this.crosshair_Image; }
    public UI_PlayerInteract UI_PlayerInteract { get => this.ui_PlayerInteract; }
    public UI_PlayerInfoScanner UI_PlayerInfoScanner { get => this.ui_PlayerInfoScanner; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.crosshair_Image == null)
            this.crosshair_Image = transform.Find("Crosshair_Image").GetComponent<Image>();

        if (this.ui_PlayerInteract == null)
            this.ui_PlayerInteract = GetComponentInChildren<UI_PlayerInteract>();

        if (this.ui_PlayerInfoScanner == null)
            this.ui_PlayerInfoScanner = GetComponentInChildren<UI_PlayerInfoScanner>();

        if (this.ui_Skill == null)
            this.ui_Skill = GetComponentInChildren<UI_Skill>();
    }
}
