using System.Collections.Generic;
using UnityEngine;

public class MM_ControlGuidePanel : BaseUIElement
{
    [Header("CONTROL GUIDE PANEL")]
    [SerializeField] private List<CanvasGroup> listGuidePanel = new List<CanvasGroup>();

    private int currentIndex = 0;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.listGuidePanel.Count == 0)
        {
            CanvasGroup[] canvasGroups = GetComponentsInChildren<CanvasGroup>();
            if (canvasGroups.Length > 1)
            {
                for (int i = 1; i < canvasGroups.Length; i++) //Skip CanvasGroup component in this gameobject
                {
                    this.listGuidePanel.Add(canvasGroups[i]);
                }
            }
        }
    }

    public override void Show(object data)
    {
        base.Show(data);

        for (int i = 0; i < this.listGuidePanel.Count; i++)
        {
            this.listGuidePanel[i].alpha = i == 0 ? 1 : 0;
        }
    }

    public void OnClickNextButton()
    {
        this.SwitchGuide(1);
    }

    public void OnClickPrevButton()
    {
        this.SwitchGuide(-1);
    }

    public void OnClickBackButton()
    {
        this.Hide();
    }

    private void SwitchGuide(int offset)
    {
        if (this.listGuidePanel.Count == 0) return;

        this.currentIndex += offset;
        if (this.currentIndex >= this.listGuidePanel.Count)
            this.currentIndex = 0;
        else if (this.currentIndex < 0)
            this.currentIndex = this.listGuidePanel.Count - 1;

        for (int i = 0; i < this.listGuidePanel.Count; i++)
        {
            this.listGuidePanel[i].alpha = this.currentIndex == i ? 1 : 0;
        }
    }
}
