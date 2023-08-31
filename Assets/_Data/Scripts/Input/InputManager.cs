using Unity.VisualScripting;
using UnityEngine;

public class InputManager : BaseManager<InputManager>
{
    [SerializeField] private Input_MainMenuScene input_MainMenuScene;
    [SerializeField] private Input_CharacterSelectionScene input_ChrSelScene;
    public Input_MainMenuScene Input_MainMenuScene { get => this.input_MainMenuScene; }
    public Input_CharacterSelectionScene Input_ChrSelScene { get => this.input_ChrSelScene; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.input_MainMenuScene == null)
            this.input_MainMenuScene = GetComponentInChildren<Input_MainMenuScene>();

        if (this.input_ChrSelScene == null)
            this.input_ChrSelScene = GetComponentInChildren<Input_CharacterSelectionScene>();
    }

    private void OnEnable()
    {
        //TODO: change it
        this.Enable_Input_MainMenuScene();
        //this.Enable_Input_ChrSelScene();
    }

    public void Disable_Input_All()
    {
        this.input_MainMenuScene.enabled = false;
        this.input_ChrSelScene.enabled = false;
    }

    public void Enable_Input_MainMenuScene()
    {
        this.Disable_Input_All();
        //MainMenuSceneCtrl
        if (this.input_MainMenuScene.MainMenuSceneCtrl == null)
        {
            MainMenuSceneCtrl mmSceneCtrl = GameObject.Find("MainMenuSceneCtrl")?.GetComponent<MainMenuSceneCtrl>();
            if (mmSceneCtrl == null) return;

            this.input_MainMenuScene.MainMenuSceneCtrl = mmSceneCtrl;
            this.input_MainMenuScene.SetupInput();
        }

        this.input_MainMenuScene.enabled = true;
    }

    public void Enable_Input_ChrSelScene()
    {
        this.Disable_Input_All();

        if (input_ChrSelScene.ChrSelSceneCtrl == null)
        {
            CharacterSelectionSceneCtrl chrSelSceneCtrl = GameObject.Find("CharacterSelectionSceneCtrl")?.GetComponent<CharacterSelectionSceneCtrl>();
            if (chrSelSceneCtrl == null) return;

            this.input_ChrSelScene.ChrSelSceneCtrl = chrSelSceneCtrl;
            this.input_ChrSelScene.SetupInput();
        }

        this.input_ChrSelScene.enabled = true;
    }
}
