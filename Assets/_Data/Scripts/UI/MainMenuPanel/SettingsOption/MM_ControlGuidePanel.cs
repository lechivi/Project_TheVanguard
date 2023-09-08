using System.Collections.Generic;
using UnityEngine;

public class MM_ControlGuidePanel : ControlGuidePanel
{
    [Header("CONTROL GUIDE PANEL")]
    [SerializeField] private UiAppear containerAppear;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.containerAppear == null)
            this.containerAppear = GetComponentInChildren<UiAppear>();
    }

    public override void Show(object data)
    {
        base.Show(data);

        this.containerAppear.Appear();
    }

    public void OnClickBackButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_GUIDECLOSE_SCROLLCLOSE);
        }

        this.Hide();

    }


}
