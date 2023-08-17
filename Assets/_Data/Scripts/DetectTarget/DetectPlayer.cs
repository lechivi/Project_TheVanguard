using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : DetectTarget
{
    public IAlliance FindClosestAlliance()
    {
        if (this.visibleTargets.Count == 0) return null;

        IAlliance closestEnemy = null;
        for (int i = 0; i < this.visibleTargets.Count; i++)
        {
            IAlliance alliance = this.visibleTargets[i].GetComponent<IAlliance>();
            if (alliance == null) continue;

            Vector3 directionToEnemy = alliance.GetCenterTransform().position - transform.position;
            if (Physics.Raycast(transform.position, directionToEnemy, this.detectionRange, this.obstacleLayer)) continue;

            if (closestEnemy == null)
            {
                closestEnemy = alliance;
            }
            else
            {
                if (Vector3.Distance(transform.position, closestEnemy.GetCenterTransform().position) >
                    Vector3.Distance(transform.position, this.visibleTargets[i].transform.position))
                {
                    closestEnemy = alliance;
                }
            }
        }

        return closestEnemy;
    }
}
