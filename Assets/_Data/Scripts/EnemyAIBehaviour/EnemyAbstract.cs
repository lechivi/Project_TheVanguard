using UnityEngine;

public abstract class EnemyAbstract : SaiMonoBehaviour
{
    [SerializeField] protected EnemyCtrl enemyCtrl;

    public EnemyCtrl EnemyCtrl { get => this.enemyCtrl; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.enemyCtrl == null)
            this.enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
    }
}
