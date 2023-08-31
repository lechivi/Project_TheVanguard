using UnityEngine;

public class InGamePanel : BaseUIElement
{
    [Header("IN GAME PANEL")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private InGame_AlwaysOnUI inGame_AlwaysOnUI;
    [SerializeField] private InGame_PauseMenu inGame_PauseMenu;
    [SerializeField] private InGame_Other inGame_Other;

    [Space(10)]
    [SerializeField] private Camera canvasCamera;

    public Camera CanvasCamera { get => this.canvasCamera; set => this.canvasCamera = value; }
    public InGame_AlwaysOnUI InGame_AlwaysOnUI { get => this.inGame_AlwaysOnUI; }
    public InGame_PauseMenu InGame_PauseMenu { get => this.inGame_PauseMenu; }
    public InGame_Other InGame_Other { get  => this.inGame_Other; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.canvas == null)
            this.canvas = GetComponent<Canvas>();

        if (this.inGame_AlwaysOnUI == null)
            this.inGame_AlwaysOnUI = GetComponentInChildren<InGame_AlwaysOnUI>();

        if (this.inGame_PauseMenu == null)
            this.inGame_PauseMenu = GetComponentInChildren<InGame_PauseMenu>();

        if (this.inGame_Other == null)
            this.inGame_Other = GetComponentInChildren<InGame_Other>();
    }

    public override void Show(object data)
    {
        base.Show(data);
        if (data is Camera)
        {
            Camera newData = data as Camera;
            this.SetCanvasCamera(newData);
        }

        this.ShowAlwaysOnUI(null);
    }

    public override void Hide()
    {
        base.Hide();
        this.inGame_AlwaysOnUI.Hide();
        this.inGame_PauseMenu.Hide();
        this.inGame_Other.Hide();
    }

    private void OnEnable()
    {
        this.ShowAlwaysOnUI(null);
    }

    private void SetCanvasCamera(Camera canvasCamera)
    {
        this.canvasCamera = canvasCamera;
        this.canvas.worldCamera = this.canvasCamera;
    }

    public void ShowAlwaysOnUI(object data)
    {
        this.inGame_AlwaysOnUI.Show(data);
        this.inGame_PauseMenu.Hide();
        this.inGame_Other.Hide();
    }

    public void ShowPauseMenu(object data)
    {
        this.inGame_AlwaysOnUI.Hide();
        this.inGame_PauseMenu.Show(data);
        this.inGame_Other.Hide();
    }

    public void ShowOther(object data)
    {
        this.inGame_AlwaysOnUI.Hide();
        this.inGame_PauseMenu.Hide();
        this.inGame_Other.Show(data);
    }
}
