using UnityEngine;

public class Input_MainMenuScene : InputControls
{
    [SerializeField] private MainMenuSceneCtrl mainMenuSceneCtrl;

    public MainMenuSceneCtrl MainMenuSceneCtrl { get => this.mainMenuSceneCtrl; set => this.mainMenuSceneCtrl = value; }

    public override void SetupInput()
    {
        base.SetupInput();

        if (this.mainMenuSceneCtrl != null)
        {
            SwitchCamera_MM switchCamera = this.mainMenuSceneCtrl.SwitchCamera;

            this.playerControls.MainMenuScene.Next.started += _ =>
            {

            };

            this.playerControls.MainMenuScene.Prev.started += _ =>
            {

            };

            this.playerControls.MainMenuScene.Up.started += _ =>
            {
                this.OnInputUp();
            };

            this.playerControls.MainMenuScene.Down.started += _ =>
            {
                this.OnInputDown();
            };        
            
            this.playerControls.MainMenuScene.Left.started += _ =>
            {
                this.OnInputLeft();
            };

            this.playerControls.MainMenuScene.Right.started += _ =>
            {
                this.OnInputRight();
            };

            this.playerControls.MainMenuScene.Back.started += _ =>
            {
                this.OnInputBack();
            };

            this.playerControls.MainMenuScene.Select.started += _ =>
            {

            };
        }
    }

    private void OnInputBack()
    {
        if (UIManager.HasInstance)
        {
            MainMenuPanel mainMenuPanel = UIManager.Instance.MainMenuPanel;
            if (UIManager.Instance.PopoutContainer.IsShow())
            {
                UIManager.Instance.PopoutContainer.OnClickNoButton();
                return;
            }

            if (mainMenuPanel.ControlGuidePanel.IsShow())
            {
                mainMenuPanel.ControlGuidePanel.OnClickBackButton();
                return;
            }

            if (mainMenuPanel.MainMenuWpPanel != null)
            {
                if (mainMenuPanel.MainMenuWpPanel.SettingsOptionPanel.IsShow())
                {
                    mainMenuPanel.MainMenuWpPanel.SettingsOptionPanel.OnClickBackButton();
                    return;
                }

            }
        }

        //if (this.mainMenuSceneCtrl)
        //{
        //    SwitchCamera switchCamera = this.mainMenuSceneCtrl.SwitchCamera;
        //    //if (switchCamera.CurrentIndex =)
        //}
    }
    
    private void OnInputUp()
    {
        SwitchCamera_MM switchCamera = this.mainMenuSceneCtrl.SwitchCamera;
        if (switchCamera.CurrentIndex != switchCamera.IndexBook)
        {
            switchCamera.SwitchPriority(0);
        }
    }      

    private void OnInputDown()
    {
        SwitchCamera_MM switchCamera = this.mainMenuSceneCtrl.SwitchCamera;
        if (switchCamera.CurrentIndex == switchCamera.IndexBook)
        {
            switchCamera.SwitchPriority(switchCamera.IndexMain);
        }
        else
        {
            switchCamera.SwitchPriority(switchCamera.IndexBook);
        }
    }    

    private void OnInputLeft()
    {
        SwitchCamera_MM switchCamera = this.mainMenuSceneCtrl.SwitchCamera;
        if (switchCamera.CurrentIndex != switchCamera.IndexBook)
        {
            switchCamera.SwitchPriority(1);
        }
    }    

    private void OnInputRight()
    {
        SwitchCamera_MM switchCamera = this.mainMenuSceneCtrl.SwitchCamera;
        if (switchCamera.CurrentIndex != switchCamera.IndexBook)
        {
            switchCamera.SwitchPriority(2);
        }
    }
}
