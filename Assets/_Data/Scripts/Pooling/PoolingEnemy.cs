using UnityEngine;

public class PoolingEnemy : PoolingObject
{
    public override bool ObjectIsActive(GameObject obj)
    {
        EnemyCtrl enemyCtrl = obj.GetComponent<EnemyCtrl>();
        if (enemyCtrl != null)
        {
            return !enemyCtrl.EnemyHealth.IsDeath() && obj.activeSelf == true;
        }
        return false;
    }

    public override void RefreshObject(GameObject obj)
    {
        EnemyCtrl enemyCtrl = obj.GetComponent<EnemyCtrl>();
        if (enemyCtrl != null)
        {
            enemyCtrl.ResetEnemy();
        }
        obj.SetActive(true);
    }
}
