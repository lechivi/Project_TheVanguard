using UnityEngine;

public class UI_ExitPanel : BaseUIElement
{
    [SerializeField] private InGame_PauseMenu pauseMenu;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.pauseMenu == null)
            this.pauseMenu = GetComponentInParent<InGame_PauseMenu>();
    }

    public void OnClickResumeButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        if (GameManager.HasInstance)
        {
            GameManager.Instance.ResumeGame();
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.InGamePanel.ShowAlwaysOnUI(null);
        }
    }

    public void OnClickMainMenuButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.PopoutContainer.ShowIG_MainMenuPopout();
        }
    }

    public void OnClickQuitButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.PopoutContainer.ShowIG_QuitPopout();
        }
    }
}
