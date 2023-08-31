using System;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    [SerializeField] private MainMenuPanel mainMenuPanel;
    [SerializeField] private LoadingPanel loadingPanel;
    [SerializeField] private CharacterSelectionPanel chrSelPanel;
    [SerializeField] private InGamePanel inGamePanel;

    public MainMenuPanel MainMenuPanel { get => this.mainMenuPanel; }
    public LoadingPanel LoadingPanel { get => this.loadingPanel; }
    public CharacterSelectionPanel ChrSelPanel { get => this.chrSelPanel; }
    public InGamePanel InGamePanel { get => this.inGamePanel; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.mainMenuPanel == null)
            this.mainMenuPanel = GetComponentInChildren<MainMenuPanel>();

        if (this.loadingPanel == null)
            this.loadingPanel = GetComponentInChildren<LoadingPanel>();

        if (this.chrSelPanel == null)
            this.chrSelPanel = GetComponentInChildren<CharacterSelectionPanel>();

        if (this.inGamePanel == null)
            this.inGamePanel = GetComponentInChildren<InGamePanel>();
    }

    private void OnEnable()
    {
        //TODO: Change it
        //this.Enable_UI_MainMenuPanel();
        //this.Enable_UI_ChrSelPanel();
        this.Enable_UI_InGamePanel();
    }

    public void Disable_UI_All()
    {
        this.mainMenuPanel.Hide();
        this.loadingPanel.Hide();
        this.chrSelPanel.Hide();
        this.inGamePanel.Hide();
    }

    public void Enable_UI_MainMenuPanel()
    {
        this.Disable_UI_All();

        if (this.mainMenuPanel.MainMenuWpPanel == null)
        {
            MainMenuWpPanel mainMenuWpPanel = GameObject.Find("MainMenuWpPanel")?.GetComponent<MainMenuWpPanel>();
            if (mainMenuWpPanel == null)
            {
                Debug.LogError("Cannot find 'MainMenuWpPanel' in this scene", gameObject);
                return;
            }

            this.mainMenuPanel.Show(mainMenuWpPanel);
            return;
        }

        this.mainMenuPanel.Show(null);
    }

    public void Enable_UI_LoadingPanel()
    {
        this.Disable_UI_All();

        this.loadingPanel.Show(null);
        //Load Scene
    }

    public void Enable_UI_ChrSelPanel()
    {
        this.Disable_UI_All();

        if (this.chrSelPanel.ChrSelSceneCtrl == null)
        {
            CharacterSelectionSceneCtrl chrSelSceneCtrl = GameObject.Find("CharacterSelectionSceneCtrl")?.GetComponent<CharacterSelectionSceneCtrl>();
            if (chrSelSceneCtrl == null)
            {
                Debug.LogError("Cannot find 'CharacterSelectionSceneCtrl' in this scene", gameObject);
                return;
            }

            this.chrSelPanel.Show(chrSelSceneCtrl);
            return;
        }

        this.chrSelPanel.Show(null);
    }

    public void Enable_UI_InGamePanel()
    {
        this.Disable_UI_All();

        if (this.inGamePanel.CanvasCamera == null)
        {
            Camera canvasCamera = GameObject.Find("CanvasCamera")?.GetComponent<Camera>();
            if (canvasCamera == null)
            {
                Debug.LogError("Cannot find 'canvasCamera' in this scene", gameObject);
                return;
            }

            this.inGamePanel.Show(canvasCamera);
            return;
        }

        this.inGamePanel.Show(null);
    }

}
