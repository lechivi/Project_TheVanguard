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
        if (UIManager.HasInstance)
        {
            UIManager.Instance.MainMenuPanel.PopoutContainer.ShowStartPopout();
        }
    }

    public void OnClickLoadButton()
    {
        Debug.Log("Click Load button");
    }

    public void OnClickSettingsButton()
    {
        this.mainMenuWpPanel.MenuOptionsPanel.Hide();
        this.mainMenuWpPanel.SettingsOptionPanel.Show(null);
    }

    public void OnClickQuitButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.MainMenuPanel.PopoutContainer.ShowQuitPopout();
        }
    }
}
