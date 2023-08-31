using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNotePanel : BaseUIElement
{
    [Header("BUTTON NOTE PANEL")]
    [SerializeField] private List<UI_ButtonNote> listButtonNote;
    [SerializeField] private List<Sprite> listSpriteKeyboard;
    [SerializeField] private List<Sprite> listSpriteGamepad;

    [SerializeField] private bool isKeyboard;

    public List<UI_ButtonNote> ListButtonNote { get => this.listButtonNote; }
    public bool IsKeyboard { get => this.isKeyboard; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        UI_ButtonNote[] buttonNotes = GetComponentsInChildren<UI_ButtonNote>();
        if (this.listButtonNote.Count != buttonNotes.Length || (this.listButtonNote == null && buttonNotes.Length > 0))
        {
            this.listButtonNote.Clear();
            foreach(UI_ButtonNote buttonNote in buttonNotes)
            {
                this.listButtonNote.Add(buttonNote);
            }
        }
    }

    private void OnEnable()
    {
        this.SetTypeInput(this.isKeyboard);
    }

    public void SetTypeInput(bool isKeyboard)
    {
        if (this.listButtonNote.Count != this.listSpriteKeyboard.Count || this.listButtonNote.Count != this.listSpriteGamepad.Count) return;

        this.isKeyboard = isKeyboard;
        if (this.isKeyboard)
        {
            for(int i = 0; i < this.listButtonNote.Count; i++)
            {
                this.listButtonNote[i].Image.sprite = this.listSpriteKeyboard[i];
            }
        }
        else
        {
            for (int i = 0; i < this.listButtonNote.Count; i++)
            {
                this.listButtonNote[i].Image.sprite = this.listSpriteGamepad[i];
            }
        }
    }
}
