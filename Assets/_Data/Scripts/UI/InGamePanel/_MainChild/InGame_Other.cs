using UnityEngine;

public class InGame_Other : BaseUIElement
{
    [Header("OTHER")]
    [SerializeField] private UI_ExchangePanel exchangePanel;
    [SerializeField] private UI_VictoryPanel victoryPanel;
    [SerializeField] private UI_LosePanel losePanel;

    public UI_ExchangePanel ExchangePanel { get => this.exchangePanel; }
    public UI_VictoryPanel VictoryPanel { get => this.victoryPanel; }
    public UI_LosePanel LosePanel { get => this.losePanel; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.exchangePanel == null)
            this.exchangePanel = GetComponentInChildren<UI_ExchangePanel>();

        if (this.victoryPanel == null)
            this.victoryPanel = GetComponentInChildren<UI_VictoryPanel>();

        if (this.losePanel == null)
            this.losePanel = GetComponentInChildren<UI_LosePanel>();
    }

    public void ShowExchangePanel()
    {
        this.exchangePanel.Show(null);
        this.victoryPanel.Hide();
        this.losePanel.Hide();
    }

    public void ShowVictoryPanel()
    {
        this.exchangePanel.Hide();
        this.victoryPanel.Show(null);
        this.losePanel.Hide();
    }

    public void ShowLosePanel()
    {
        this.exchangePanel.Hide();
        this.victoryPanel.Hide();
        this.losePanel.Show(null);
    }
}
