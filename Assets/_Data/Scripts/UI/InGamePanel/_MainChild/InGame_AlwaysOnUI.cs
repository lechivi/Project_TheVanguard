using UnityEngine;
using UnityEngine.UI;

public class InGame_AlwaysOnUi : BaseUIElement
{
    [Header("ALWAYS ON UI")]
    [SerializeField] private Scope scope;
    [SerializeField] private Crosshair crosshair;
    [SerializeField] private UI_PlayerInteract ui_PlayerInteract;
    [SerializeField] private UI_PlayerInfoScanner ui_PlayerInfoScanner;
    [SerializeField] private UI_Skill ui_Skill;

    public Scope Scope { get => this.scope; }
    public Crosshair Crosshair { get => this.crosshair; }
    public UI_PlayerInteract UI_PlayerInteract { get => this.ui_PlayerInteract; }
    public UI_PlayerInfoScanner UI_PlayerInfoScanner { get => this.ui_PlayerInfoScanner; }
    public UI_Skill UI_Skill { get => this.ui_Skill; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.scope == null)
            this.scope = GetComponentInChildren<Scope>();

        if (this.crosshair == null)
            this.crosshair = GetComponentInChildren<Crosshair>();

        if (this.ui_PlayerInteract == null)
            this.ui_PlayerInteract = GetComponentInChildren<UI_PlayerInteract>();

        if (this.ui_PlayerInfoScanner == null)
            this.ui_PlayerInfoScanner = GetComponentInChildren<UI_PlayerInfoScanner>();

        if (this.ui_Skill == null)
            this.ui_Skill = GetComponentInChildren<UI_Skill>();
    }

    public override void Show(object data)
    {
        base.Show(data);

        this.scope.Hide();
        this.crosshair.Show(null);
        this.ui_PlayerInteract.Hide();
        this.ui_PlayerInfoScanner.Hide();
        this.ui_Skill.Show(null);
    }
}
