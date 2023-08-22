using UnityEngine;

public class AlliancePlayer_InfoScanner : SaiMonoBehaviour, IInfoScanner
{
    [SerializeField] private Transform centerPoint;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.centerPoint == null)
            this.centerPoint = transform.Find("CenterPoint");
    }

    public FactionType GetFactionType()
    {
        return FactionType.Alliance;
    }

    public Transform GetCenterPoint()
    {
        return this.centerPoint;
    }

    public string GetTargetName()
    {
        return gameObject.name;
    }

    public int GetTargetLevel()
    {
        return 1;
    }

    public IHealth GetHealth()
    {
        return PlayerCtrl.Instance.PlayerHealth;
    }

    public bool CanScan()
    {
        return !PlayerCtrl.Instance.PlayerHealth.IsDeath();
    }
}
