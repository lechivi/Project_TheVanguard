using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : DetectTarget
{
    public EnemyCtrl FindClosestEnemyCtrl()
    {
        if (this.visibleTargets.Count == 0) return null;

        EnemyCtrl closestEnemy = null;
        for (int i = 0; i < this.visibleTargets.Count; i++)
        {
            EnemyCtrl enemy = this.visibleTargets[i].GetComponent<EnemyCtrl>();
            if (!enemy) continue;

            Vector3 directionToEnemy = enemy.CenterPoint.position - transform.position;
            if (Physics.Raycast(transform.position, directionToEnemy, this.detectionRange, this.obstacleLayer)) continue;

            if (closestEnemy == null)
            {
                closestEnemy = enemy;
            }
            else
            {
                if (Vector3.Distance(transform.position, closestEnemy.transform.position) >
                    Vector3.Distance(transform.position, this.visibleTargets[i].transform.position))
                {
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }
}
