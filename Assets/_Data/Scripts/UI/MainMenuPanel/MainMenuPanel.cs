using UnityEngine;

public class MainMenuPanel : BaseUIElement
{
    [Header("MAIN MENU PANEL")]
    [SerializeField] private MM_ControlGuidePanel controlGuidePanel;
    [SerializeField] private MainMenuWpPanel mainMenuWpPanel;

    public MM_ControlGuidePanel ControlGuidePanel { get => this.controlGuidePanel; }
    public MainMenuWpPanel MainMenuWpPanel { get => this.mainMenuWpPanel; set => this.mainMenuWpPanel = value; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.controlGuidePanel == null)
            this.controlGuidePanel = GetComponentInChildren<MM_ControlGuidePanel>();
    }

    public override void Show(object data)
    {
        base.Show(data);

        //if (data is MainMenuWpPanel)
        //{
        //    MainMenuWpPanel newData = data as MainMenuWpPanel;
        //    this.mainMenuWpPanel = newData;
        //}
    }

    public void SetInteractMainMenuWp(bool isInteract)
    {
        if (this.mainMenuWpPanel)
        {
            this.mainMenuWpPanel.SetInteract(isInteract);
        }
    }

    //private Stack<string> windowHistory = new Stack<string>();
    //private string currentWindow;

    //public void AddWindow(string optionName)
    //{
    //    this.windowHistory.Push(optionName);
    //    this.currentWindow = optionName;
    //}

    //public void CloseWindow()
    //{
    //    if (this.windowHistory.Count > 0)
    //    {
    //        this.windowHistory.Pop();
    //        this.currentWindow = this.windowHistory.Peek();
    //    }
    //}
}
