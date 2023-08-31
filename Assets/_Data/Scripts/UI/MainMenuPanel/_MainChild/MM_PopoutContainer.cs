using UnityEngine;

public class MM_PopoutContainer : BaseUIElement
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

    private void OnEnable()
    {
        this.Hide();
    }

    private void SetActiveCanvasGroup(CanvasGroup canvasGroup, bool isActive)
    {
        canvasGroup.alpha = isActive ? 1 : 0;
        canvasGroup.blocksRaycasts = isActive;
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

    public void OnClickNoButton()
    {
        this.Hide();
    }

    public void OnClickStartPopout_YesButton()
    {
        Debug.Log("Click Yes Start button");
    }

    public void OnClickQuitPopout_YesButton()
    {
        Debug.Log("Click Yes Quit button");
    }
}
