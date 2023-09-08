using UnityEngine;

public class MM_MenuOptionsPanel : BaseUIElement
{
    [Header("MENU OPTIONS")]
    [SerializeField] private MainMenuWpPanel mainMenuWpPanel;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.mainMenuWpPanel == null)
            this.mainMenuWpPanel = GetComponentInParent<MainMenuWpPanel>();
    }


    public void OnClickStartButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_BOOK_01_PAGE_TURN_14);
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.PopoutContainer.ShowMM_StartPopout();
        }
    }

    public void OnClickLoadButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_BOOK_01_PAGE_TURN_14);
        }

        Debug.Log("Click Load button");
    }

    public void OnClickSettingsButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_BOOK_01_PAGE_TURN_14);
        }

        this.mainMenuWpPanel.MenuOptionsPanel.Hide();
        this.mainMenuWpPanel.SettingsOptionPanel.Show(null);
    }

    public void OnClickQuitButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_BOOK_01_PAGE_TURN_14);
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.PopoutContainer.ShowMM_QuitPopout();
        }
    }
}
