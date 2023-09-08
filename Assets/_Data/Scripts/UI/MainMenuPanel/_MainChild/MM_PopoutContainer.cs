using UnityEngine;

public class MM_PopoutContainer : PopoutContainer
{
    [Header("POPOUT CONTAINER")]
    [SerializeField] private CanvasGroup startPopout;
    [SerializeField] private CanvasGroup quitPopout;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.startPopout == null)
            this.startPopout = transform.Find("Start_Popout").GetComponent<CanvasGroup>();

        if (this.quitPopout == null)
            this.quitPopout = transform.Find("Quit_Popout").GetComponent<CanvasGroup>();
    }

    public void ShowStartPopout()
    {
        this.Show(null);
        this.SetActiveCanvasGroup(this.startPopout, true);
        this.SetActiveCanvasGroup(this.quitPopout, false);
    }

    public void ShowQuitPopout()
    {
        this.Show(null);
        this.SetActiveCanvasGroup(this.startPopout, false);
        this.SetActiveCanvasGroup(this.quitPopout, true);
    }
}
