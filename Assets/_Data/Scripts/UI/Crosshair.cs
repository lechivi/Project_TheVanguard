using UnityEngine;
using UnityEngine.UI;

public class Crosshair : BaseUIElement
{
    [Header("CROSSHAIR")]
    [SerializeField] private Image image;
    [SerializeField] private Color noneColor = Color.white;
    [SerializeField] private Color allyColor = Color.green;
    [SerializeField] private Color enemyColor = Color.red;

    private FactionType currentTarget;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.image == null)
            this.image = GetComponent<Image>();
    }

    public void SetCrosshairTarget(FactionType factionTarget)
    {
        if (this.currentTarget == factionTarget) return;

        this.currentTarget = factionTarget;
        switch (this.currentTarget)
        {
            case FactionType.Unknow:
                this.image.color = noneColor; 
                break;
            case FactionType.Alliance:
                this.image.color = allyColor; 
                break;
            case FactionType.Voidspawn:
                this.image.color = enemyColor;
                break;
        }
    }
}
