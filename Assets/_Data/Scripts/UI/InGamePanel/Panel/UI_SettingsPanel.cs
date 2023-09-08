using UnityEngine;

public class UI_SettingsPanel : BaseUIElement
{
    [Header("SETTINGS PANEL")]
    [SerializeField] private OptionSelection optionSelection;
    [SerializeField] private AudioTab audioTab;
    [SerializeField] private GraphicTab graphicTab;
    [SerializeField] private ControlGuidePanel controlTab;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.optionSelection == null)
            this.optionSelection = GetComponent<OptionSelection>();

        if (this.audioTab == null)
            this.audioTab = GetComponentInChildren<AudioTab>();

        if (this.graphicTab == null)
            this.graphicTab = GetComponentInChildren<GraphicTab>();

        if (this.controlTab == null)
            this.controlTab = GetComponentInChildren<ControlGuidePanel>();
    }

    private void OnEnable()
    {
        this.optionSelection.SetSelectOption(0);
        this.audioTab.Show(null);
        this.graphicTab.Hide();
        this.controlTab.Hide();
    }

    public void OnClickAudioTabButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        this.audioTab.Show(null);
        this.graphicTab.Hide();
        this.controlTab.Hide();
        this.optionSelection.SetSelectOption(0);
    }

    public void OnClickGraphicTabButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        this.audioTab.Hide();
        this.graphicTab.Show(null);
        this.controlTab.Hide();
        this.optionSelection.SetSelectOption(1);
    }

    public void OnClickControlTabButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        this.audioTab.Hide();
        this.graphicTab.Hide();
        this.controlTab.Show(null);
        this.optionSelection.SetSelectOption(2);
    }
}
