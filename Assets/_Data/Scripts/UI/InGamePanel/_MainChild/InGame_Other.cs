using UnityEngine;

public class InGame_Other : BaseUIElement
{
    [Header("OTHER")]
    [SerializeField] private UI_ExchangePanel ui_ExchangePanel;

    public UI_ExchangePanel UI_ExchangePanel { get => this.ui_ExchangePanel; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.ui_ExchangePanel == null)
            this.ui_ExchangePanel = GetComponentInChildren<UI_ExchangePanel>();
    }
}
