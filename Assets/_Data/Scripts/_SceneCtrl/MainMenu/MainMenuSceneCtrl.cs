using UnityEngine;

public class MainMenuSceneCtrl : SaiMonoBehaviour
{
    [SerializeField] private MainMenuWpPanel mainMenuWpPanel;
    [SerializeField] private SwitchCamera_MM switchCamera;
    [SerializeField] private GameObject menuBookClose;
    [SerializeField] private GameObject menuBookOpen;

    private bool isOpen = false;

    public MainMenuWpPanel MainMenuWpPanel { get => this.mainMenuWpPanel; }
    public SwitchCamera_MM SwitchCamera { get => this.switchCamera; }
    public bool IsOpen { get => this.isOpen; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.mainMenuWpPanel == null)
            this.mainMenuWpPanel = GameObject.Find("MainMenuWpPanel").GetComponent<MainMenuWpPanel>();

        if (this.switchCamera == null)
            this.switchCamera = GameObject.Find("SwitchCamera").GetComponent<SwitchCamera_MM>();

        if (this.menuBookClose == null)
            this.menuBookClose = GameObject.Find("MenuBook_Close");

        if (this.menuBookOpen == null)
            this.menuBookOpen = GameObject.Find("MenuBook_Open");
    }

    private void Start()
    {
        this.SetBook(this.isOpen);
        if (UIManager.HasInstance)
        {
            UIManager.Instance.Enable_UI_MainMenuPanel();
        }
        if (InputManager.HasInstance)
        {
            InputManager.Instance.Enable_Input_MainMenuScene();
        }

        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayBgm(AUDIO.BGM_MAINMENU_ZANFONAOFDOOM);
        }

        if (GameManager.HasInstance)
        {
            GameManager.Instance.IsShowCursor(true);
        }
    }

    public void SetBook(bool isOpen)
    {
        this.isOpen = isOpen;
        if (this.isOpen)
        {
            this.menuBookClose.SetActive(false);
            this.menuBookOpen.SetActive(true);
        }
        else
        {
            this.menuBookClose.SetActive(true);
            this.menuBookOpen.SetActive(false);
        }
    }
}
