using UnityEngine;

public class ChrSel_ButtonNotePanel : ButtonNotePanel
{
    [Header("CHR SEL")]
    [SerializeField] private UI_ButtonNote selectButtonNote;
    [SerializeField] private UI_ButtonNote startButtonNote;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.selectButtonNote == null)
        {
            foreach (UI_ButtonNote bn in this.ListButtonNote)
            {
                if (bn.name == "Select")
                {
                    this.selectButtonNote = bn;
                    break;
                }
            }

        }     
        
        if (this.startButtonNote == null)
        {
            foreach (UI_ButtonNote bn in this.ListButtonNote)
            {
                if (bn.name == "Start")
                {
                    this.startButtonNote = bn;
                    break;
                }
            }
        }
    }

    public void SetActive_SelectButtonNote(bool enable)
    {
        if (enable)
        {
            this.selectButtonNote.Show(null);
        }
        else
        {
            this.selectButtonNote.Hide();
        }
    }

    public void SetSelect_SelectButtonNote(bool select)
    {
        string newText = select ? "Select" : "Deselect";
        if (this.selectButtonNote.Text.ToString() != newText)
        {
            this.selectButtonNote.Text.SetText(newText);
        }
    }

    public void SetActive_StartButtonNote(bool enable)
    {
        if (enable)
        {
            this.startButtonNote.Show(null);
        }
        else
        {
            this.startButtonNote.Hide();
        }
    }
}
