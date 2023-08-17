using System.Collections.Generic;
using UnityEngine;

public class ScannerEnemy : MonoBehaviour
{
    [SerializeField] private int maxScanTimes = 3;
    [SerializeField] private float scanRange = 3f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private List<EnemyCtrl> enemies = new List<EnemyCtrl>();

    public int MaxSearchTimes { get => this.maxScanTimes; set => this.maxScanTimes = value; }
    public float SearchRadius { get => this.scanRange; set => this.scanRange = value; }
    public List<EnemyCtrl> Enemies { get => this.enemies; }

    public void Scan(EnemyCtrl firstEnemy, int maxScanTimes, float scanRange)
    {
        this.maxScanTimes = maxScanTimes;
        this.scanRange = scanRange;
        this.enemies.Clear();
        this.enemies.Add(firstEnemy);
        this.SearchForClosestEnemy(firstEnemy.CenterPoint, this.maxScanTimes - 1);
    }

    private void SearchForClosestEnemy(Transform origin, int searchTimeLeft)
    {
        if (searchTimeLeft <= 0) return;

        Collider[] hitCollider = Physics.OverlapSphere(origin.position, this.scanRange, this.enemyLayer);
        EnemyCtrl closestEnemy = null;

        for (int i = 0; i < hitCollider.Length; i++)
        {
            EnemyCtrl enemyCtrl = hitCollider[i].transform.GetComponent<EnemyCtrl>();
            if (enemyCtrl == null || this.enemies.Contains(enemyCtrl)) continue;

            Vector3 directionToEnemy = enemyCtrl.CenterPoint.position - origin.position;
            if (Physics.Raycast(origin.position, directionToEnemy, this.scanRange, this.obstacleLayer)) continue;

            if (closestEnemy == null)
            {
                closestEnemy = enemyCtrl;
            }
            else
            {
                if (Vector3.Distance(origin.position, closestEnemy.transform.position) >
                    Vector3.Distance(origin.position, enemyCtrl.transform.position))
                {
                    closestEnemy = enemyCtrl;
                }
            }
        }

        if (closestEnemy != null)
        {
            this.enemies.Add(closestEnemy);
            this.SearchForClosestEnemy(closestEnemy.CenterPoint, searchTimeLeft - 1);
        }

    }
}
