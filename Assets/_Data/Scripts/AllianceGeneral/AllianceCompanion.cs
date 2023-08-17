using UnityEngine;

public class AllianceCompanion : SaiMonoBehaviour, IAlliance
{
    [SerializeField] private Transform centerPoint;
    //[SerializeField] private IAllianceHealth health;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.centerPoint == null)
        {
            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                if (t.gameObject.name == "CenterPoint")
                {
                    this.centerPoint = t;
                    break;
                }
            }

        }
    }

    public AllianceType GetAllianceType()
    {
        return AllianceType.Companion;
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
