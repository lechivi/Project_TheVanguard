using UnityEngine;

public class MainMenuWpPanel : BaseUIElement
{
    [Header("MAIN MENU WP PANEL")]
    [SerializeField] private MM_MenuOptionsPanel menuOptionsPanel;
    [SerializeField] private MM_SettingsOptionPanel settingsOptionPanel;
    [SerializeField] private AudioTab audioTab;
    [SerializeField] private GraphicTab graphicTab;
    [SerializeField] private ControlTab controlTab;

    public MM_MenuOptionsPanel MenuOptionsPanel { get => this.menuOptionsPanel; }
    public MM_SettingsOptionPanel SettingsOptionPanel { get => this.settingsOptionPanel; }
    public AudioTab AudioTab { get => this.audioTab; }
    public GraphicTab GraphicTab { get => this.graphicTab; }
    public ControlTab ControlTab { get => this.controlTab; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.menuOptionsPanel == null ) 
            this.menuOptionsPanel = GetComponentInChildren<MM_MenuOptionsPanel>();

        if (this.settingsOptionPanel == null)
            this.settingsOptionPanel = GetComponentInChildren<MM_SettingsOptionPanel>();

        if (this.audioTab == null)
            this.audioTab = GetComponentInChildren<AudioTab>();

        if (this.graphicTab == null)
            this.graphicTab = GetComponentInChildren<GraphicTab>();

        if (this.controlTab == null)
            this.controlTab = GetComponentInChildren<ControlTab>();
    }

    private void OnEnable()
    {
        this.Show(null);
    }

    public override void Show(object data)
    {
        base.Show(data);
        this.menuOptionsPanel.Show(data);
        this.settingsOptionPanel.Hide();
        this.audioTab.Hide();
        this.graphicTab.Hide();
        this.controlTab.Hide();
    }

    public override void Hide()
    {
        base.Hide();
        this.menuOptionsPanel.Hide();
        this.settingsOptionPanel.Hide();
        this.audioTab.Hide();
        this.graphicTab.Hide();
        this.controlTab.Hide();
    }
}
