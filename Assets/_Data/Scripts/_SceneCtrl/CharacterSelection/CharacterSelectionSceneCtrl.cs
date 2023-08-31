using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionSceneCtrl : SaiMonoBehaviour
{
    [SerializeField] private SwitchCamera_ChrSel switchCamera;
    [SerializeField] private List<CharacterSelectable> listCharacterSelectable;

    [SerializeField] private int indexSelected = -1;

    public SwitchCamera_ChrSel SwitchCamera { get => this.switchCamera; }
    public int IndexSelected { get => this.indexSelected; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.switchCamera == null)
            this.switchCamera = GameObject.Find("SwitchCamera").GetComponent<SwitchCamera_ChrSel>();
    }

    private void OnEnable()
    {
        this.CheckUI_ChrInfoPanel(this.switchCamera.CurrentIndex);
    }

    public void SetSelection(int index)
    {
        if (this.indexSelected == index)
        {
            this.listCharacterSelectable[this.indexSelected].SelectedFx.gameObject.SetActive(false);
            this.indexSelected = -1;
            //Remove CharacterDataSO from GameManager
        }
        else
        {
            if (this.indexSelected != -1)
            {
                this.listCharacterSelectable[this.indexSelected].SelectedFx.gameObject.SetActive(false);
                //Remove CharacterDataSO from GameManager
            }

            this.indexSelected = index;

            if (this.indexSelected != -1)
            {
                this.listCharacterSelectable[this.indexSelected].SelectedFx.gameObject.SetActive(true);
                this.listCharacterSelectable[this.indexSelected].SelectedFx.Play();
                //Add CharacterDataSO to GameManager -> PlayerCtrl
            }

        }

        this.CheckUI_ChrInfoPanel(index);
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
