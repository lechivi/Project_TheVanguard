using UnityEngine;

public class AlliancePlayer : SaiMonoBehaviour, IAlliance
{
    [SerializeField] private Transform centerPoint;
    //[SerializeField] private IAllianceHealth health;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.centerPoint == null)
            this.centerPoint = transform.Find("CenterPoint");
    }

    public AllianceType GetAllianceType()
    {
        return AllianceType.Player;
    }

    public Transform GetCenterTransform()
    {
        return this.centerPoint;
    }

    public void TakeDamge(int damage)
    {
        //this.health.TakeDamage(damage);
    }
}
