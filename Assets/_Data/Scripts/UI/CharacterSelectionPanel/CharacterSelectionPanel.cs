using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionPanel : BaseUIElement
{
    [Header("CHR SELECTION PANEL")]
    [SerializeField] private List<CharacterDataSO> listCharacterData = new List<CharacterDataSO>();
    [SerializeField] private ChrSel_ChrInfoPanel chrInfoPanel;
    [SerializeField] private ChrSel_ButtonNotePanel buttonNotePanel;

    [Space(10)]
    [SerializeField] private CharacterSelectionSceneCtrl chrSelSceneCtrl;

    public ChrSel_ButtonNotePanel ButtonNotePanel { get => this.buttonNotePanel; }
    public CharacterSelectionSceneCtrl ChrSelSceneCtrl { get => this.chrSelSceneCtrl; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.chrInfoPanel == null)
            this.chrInfoPanel = GetComponentInChildren<ChrSel_ChrInfoPanel>();

        if (this.buttonNotePanel == null)
            this.buttonNotePanel = GetComponentInChildren<ChrSel_ButtonNotePanel>();
    }

    public override void Show(object data)
    {
        base.Show(data);
        this.chrInfoPanel.Hide();
        this.buttonNotePanel.Show(null);

        if (data is CharacterSelectionSceneCtrl)
        {
            CharacterSelectionSceneCtrl newData = data as CharacterSelectionSceneCtrl;
            this.chrSelSceneCtrl = newData;
        }
    }

    public override void Hide()
    {
        base.Hide();
        this.chrInfoPanel.Hide();
        this.buttonNotePanel.Hide();
    }

    public void DisplayChrInfo(int index)
    {
        if (index == this.chrSelSceneCtrl.SwitchCamera.IndexMain)
        {
            this.chrInfoPanel.Hide();
            return;
        }

        CharacterDataSO characterData = this.listCharacterData[index];
        if (characterData == null)
        {
            this.chrInfoPanel.Hide();
            return;
        }

        this.chrInfoPanel.Show(null);
        this.chrInfoPanel.DisplayChrInfo(characterData);
    }

}
