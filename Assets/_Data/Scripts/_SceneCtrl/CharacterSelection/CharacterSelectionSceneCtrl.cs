using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionSceneCtrl : SaiMonoBehaviour
{
    [SerializeField] private List<CharacterDataSO> listCharacterData = new List<CharacterDataSO>();
    [SerializeField] private SwitchCamera_ChrSel switchCamera;
    [SerializeField] private List<CharacterSelectable> listCharacterSelectable;

    [SerializeField] private int indexSelected = -1;

    public List<CharacterDataSO> ListCharacterData { get => this.listCharacterData; }
    public SwitchCamera_ChrSel SwitchCamera { get => this.switchCamera; }
    public int IndexSelected { get => this.indexSelected; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.switchCamera == null)
            this.switchCamera = GameObject.Find("SwitchCamera").GetComponent<SwitchCamera_ChrSel>();
    }

    private void Start()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.Enable_UI_ChrSelPanel();
        }
        if (InputManager.HasInstance)
        {
            InputManager.Instance.Enable_Input_ChrSelScene();
        }

        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayBgm(AUDIO.BGM_CHRSEL_FORESTINSTRUMENTALAMB_DAY);
        }

        this.CheckUI_ChrInfoPanel(this.switchCamera.CurrentIndex);
    }

    public void SetSelection(int index)
    {
        if (this.indexSelected == index)
        {
            this.listCharacterSelectable[this.indexSelected].SelectedFx.gameObject.SetActive(false);
            this.indexSelected = -1;
        }
        else
        {
            if (this.indexSelected != -1)
            {
                this.listCharacterSelectable[this.indexSelected].SelectedFx.gameObject.SetActive(false);
            }

            this.indexSelected = index;

            if (this.indexSelected != -1)
            {
                this.listCharacterSelectable[this.indexSelected].SelectedFx.gameObject.SetActive(true);
                this.listCharacterSelectable[this.indexSelected].SelectedFx.Play();
            }

        }

        this.CheckUI_ChrInfoPanel(index);

        if (GameManager.HasInstance)
        {
            GameManager.Instance.CharacterData = this.indexSelected == -1 ? null : this.listCharacterData[this.indexSelected];
        }

    }

    public void CheckUI_ChrInfoPanel(int index)
    {
        if (UIManager.HasInstance)
        {
            CharacterSelectionPanel chrSelPanel = UIManager.Instance.ChrSelPanel;

            //Check if 'switchCamera' is viewing a character. If it is, show 'Select-Button-Note'
            if (this.switchCamera.CurrentIndex != this.switchCamera.IndexMain)
            {
                if (InputManager.HasInstance)
                {
                    InputManager.Instance.Input_ChrSelScene.CanSelect = true;
                }
                chrSelPanel.ButtonNotePanel.SetActive_SelectButtonNote(true);
            }
            else
            {
                if (InputManager.HasInstance)
                {
                    InputManager.Instance.Input_ChrSelScene.CanSelect = false;
                }
                chrSelPanel.ButtonNotePanel.SetActive_SelectButtonNote(false);
            }

            //Check if current character is selectd ? "Deselect" : "Select"
            if (this.indexSelected == index)
            {
                chrSelPanel.ButtonNotePanel.SetSelect_SelectButtonNote(false);
            }
            else
            {
                chrSelPanel.ButtonNotePanel.SetSelect_SelectButtonNote(true);
            }

            //Check if we have 'Character-Selected'. If we do, show 'Start-Game'
            if (this.indexSelected != -1)
            {
                if (InputManager.HasInstance)
                {
                    InputManager.Instance.Input_ChrSelScene.CanStart = true;
                }
                chrSelPanel.ButtonNotePanel.SetActive_StartButtonNote(true);
            }
            else
            {
                if (InputManager.HasInstance)
                {
                    InputManager.Instance.Input_ChrSelScene.CanStart = false;
                }
                chrSelPanel.ButtonNotePanel.SetActive_StartButtonNote(false);
            }
        }


    }
}
