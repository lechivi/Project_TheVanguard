using UnityEngine;

public class Input_CharacterSelectionScene : InputControls
{
    [SerializeField] private CharacterSelectionSceneCtrl chrSelSceneCtrl;

    private bool canSelect;
    private bool canStart;

    public bool CanSelect { get => this.canSelect; set => this.canSelect = value; }
    public bool CanStart { get => this.canStart; set => this.canStart = value; }

    public CharacterSelectionSceneCtrl ChrSelSceneCtrl { get => this.chrSelSceneCtrl; set => this.chrSelSceneCtrl = value; }

    public override void SetupInput()
    {
        base.SetupInput();

        if (this.chrSelSceneCtrl != null)
        {
            SwitchCamera_ChrSel switchCamera = this.chrSelSceneCtrl.SwitchCamera;

            this.playerControls.ChrSelScene.Next.started += _ =>
            {
                switchCamera.SwitchVirtualCamera(1);
            };

            this.playerControls.ChrSelScene.Prev.started += _ =>
            {
                switchCamera.SwitchVirtualCamera(-1);
            };

            this.playerControls.ChrSelScene.Back.started += _ =>
            {
                if (switchCamera.CurrentIndex != switchCamera.IndexMain)
                {
                    switchCamera.SwitchPriority(switchCamera.IndexMain);
                }
            };

            this.playerControls.ChrSelScene.Select.started += _ =>
            {
                if (this.canSelect)
                {
                    if (switchCamera.CurrentIndex != switchCamera.IndexMain)
                    {
                        this.chrSelSceneCtrl.SetSelection(switchCamera.CurrentIndex);
                    }
                }
            };

            this.playerControls.ChrSelScene.Start.started += _ =>
            {
                if (this.canStart)
                {
                    Debug.Log("Start Game");
                    //Start Game
                }
            };
        }
    }
}
