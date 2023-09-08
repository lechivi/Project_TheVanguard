using UnityEngine;

public class MM_SettingsOptionPanel : BaseUIElement
{
    [Header("SETTINGS OPTION")]
    [SerializeField] private MainMenuWpPanel mainMenuWpPanel;
    [SerializeField] private OptionSelection optionSelection;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.mainMenuWpPanel == null)
            this.mainMenuWpPanel = GetComponentInParent<MainMenuWpPanel>();

        if (this.optionSelection == null)
            this.optionSelection = GetComponent<OptionSelection>();
    }

    private void OnEnable()
    {
        this.mainMenuWpPanel.ShowAudioTab();
        this.optionSelection.SetSelectOption(0);
    }



    public void OnClickAudioButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_BOOK_01_PAGE_TURN_14);
        }

        this.mainMenuWpPanel.ShowAudioTab();
        this.optionSelection.SetSelectOption(0);
    }

    public void OnClickGraphicButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_BOOK_01_PAGE_TURN_14);
        }

        this.mainMenuWpPanel.ShowGraphicTab();
        this.optionSelection.SetSelectOption(1);
    }

    public void OnClickControlButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_GUIDEOPEN_SCROLLOPEN);
        }

        this.mainMenuWpPanel.ShowControlTab();
        this.optionSelection.SetSelectOption(2);

        if (UIManager.HasInstance)
        {
            UIManager.Instance.MainMenuPanel.ControlGuidePanel.Show(null);
        }
    }

    public void OnClickBackButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        this.mainMenuWpPanel.HideAllTab();
        this.Hide();
        this.mainMenuWpPanel.MenuOptionsPanel.Show(null);
    }
}
