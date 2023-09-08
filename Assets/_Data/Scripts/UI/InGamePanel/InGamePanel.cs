using UnityEngine;

public class InGamePanel : BaseUIElement
{
    [Header("IN GAME PANEL")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private InGame_AlwaysOnUI alwaysOnUI;
    [SerializeField] private InGame_PauseMenu pauseMenu;
    [SerializeField] private InGame_Other other;

    [Space(10)]
    [SerializeField] private Camera canvasCamera;

    public Camera CanvasCamera { get => this.canvasCamera; set => this.canvasCamera = value; }
    public InGame_AlwaysOnUI AlwaysOnUI { get => this.alwaysOnUI; }
    public InGame_PauseMenu PauseMenu { get => this.pauseMenu; }
    public InGame_Other Other { get  => this.other; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.canvas == null)
            this.canvas = GetComponent<Canvas>();

        if (this.alwaysOnUI == null)
            this.alwaysOnUI = GetComponentInChildren<InGame_AlwaysOnUI>();

        if (this.pauseMenu == null)
            this.pauseMenu = GetComponentInChildren<InGame_PauseMenu>();

        if (this.other == null)
            this.other = GetComponentInChildren<InGame_Other>();
    }

    public override void Show(object data)
    {
        base.Show(data);
        this.ShowAlwaysOnUI(null);
    }

    public override void Hide()
    {
        base.Hide();
        this.alwaysOnUI.Hide();
        this.pauseMenu.Hide();
        this.other.Hide();
    }

    private void OnEnable()
    {
        this.ShowAlwaysOnUI(null);
    }

    public void SetCanvasCamera(Camera canvasCamera)
    {
        this.canvasCamera = canvasCamera;
        this.canvas.worldCamera = this.canvasCamera;
    }

    public void ShowAlwaysOnUI(object data)
    {
        this.alwaysOnUI.Show(data);
        this.pauseMenu.Hide();
        this.other.Hide();
    }

    public void ShowPauseMenu(object data)
    {
        this.alwaysOnUI.Hide();
        this.pauseMenu.Show(data);
        this.other.Hide();
    }

    public void ShowOther(object data)
    {
        this.alwaysOnUI.Hide();
        this.pauseMenu.Hide();
        this.other.Show(data);
    }
}
