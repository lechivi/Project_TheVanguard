using UnityEngine;

public class InGame_PauseMenu : BaseUIElement
{
    [Header("PAUSE MENU")]
    [SerializeField] private OptionSelection optionSelection;
    [SerializeField] private UI_InventoryPanel inventoryPanel;
    [SerializeField] private UI_SettingsPanel settingsPanel;
    [SerializeField] private UI_ExitPanel exitPanel;

    public UI_InventoryPanel InventoryPanel { get => inventoryPanel; }
    public UI_SettingsPanel SettingsPanel { get => settingsPanel; }
    public UI_ExitPanel ExitPanel { get => exitPanel; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.optionSelection == null)
            this.optionSelection = GetComponentInChildren<OptionSelection>();

        if (this.inventoryPanel == null)
            this.inventoryPanel = GetComponentInChildren<UI_InventoryPanel>();

        if (this.settingsPanel == null)
            this.settingsPanel = GetComponentInChildren<UI_SettingsPanel>();

        if (this.exitPanel == null)
            this.exitPanel = GetComponentInChildren<UI_ExitPanel>();
    }

    private void OnEnable()
    {
        this.HideAllPanel();
        this.ShowInventoryPanel();
    }

    public void HideAllPanel()
    {
        this.inventoryPanel.Hide();
        this.settingsPanel.Hide();
        this.exitPanel.Hide();
    }

    public void ShowInventoryPanel()
    {
        this.HideAllPanel();
        this.inventoryPanel.Show(null);
        this.optionSelection.SetSelectOption(0);
    }

    public void ShowSettingsPanel()
    {
        this.HideAllPanel();
        this.settingsPanel.Show(null);
        this.optionSelection.SetSelectOption(1);
    }

    public void ShowExitPanel()
    {
        this.HideAllPanel();
        this.exitPanel.Show(null);
        this.optionSelection.SetSelectOption(2);
    }

    public void OnClickInventoryButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_BOOK_01_PAGE_TURN_14);
        }

        this.ShowInventoryPanel();
    }   
    
    public void OnClickSettingsButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_BOOK_01_PAGE_TURN_14);
        }

        this.ShowSettingsPanel();
    }   
    
    public void OnClickExitButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_BOOK_01_PAGE_TURN_14);
        }

        this.ShowExitPanel();
    }
}
