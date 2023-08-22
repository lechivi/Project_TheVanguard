using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    [SerializeField] private InGamePanel inGamePanel;

    public InGamePanel InGamePanel { get => this.inGamePanel; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.inGamePanel == null)
            this.inGamePanel = GetComponentInChildren<InGamePanel>();
    }

}
