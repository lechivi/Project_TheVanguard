using UnityEngine;

public class InGame_PopoutContainer : PopoutContainer
{
    [Header("POPOUT CONTAINER")]
    [SerializeField] private CanvasGroup mainMenuPopout;
    [SerializeField] private CanvasGroup quitPopout;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.mainMenuPopout == null)
            this.mainMenuPopout = transform.Find("MainMenu_Popout").GetComponent<CanvasGroup>();

        if (this.quitPopout == null)
            this.quitPopout = transform.Find("Quit_Popout").GetComponent<CanvasGroup>();
    }

    public void ShowMainMenuPopout()
    {
        this.Show(null);
        this.SetActiveCanvasGroup(this.mainMenuPopout, true);
        this.SetActiveCanvasGroup(this.quitPopout, false);
    }

    public void ShowQuitPopout()
    {
        this.Show(null);
        this.SetActiveCanvasGroup(this.mainMenuPopout, false);
        this.SetActiveCanvasGroup(this.quitPopout, true);
    }
}
