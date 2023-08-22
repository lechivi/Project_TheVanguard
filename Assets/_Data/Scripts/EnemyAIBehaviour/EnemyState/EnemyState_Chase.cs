using UnityEngine;
using UnityEngine.AI;

public class EnemyState_Chase : MonoBehaviour, IEnemyState
{
    private Enemy_AiCtrl enemyAiCtrl;
    private float timer = 0;

    public EnemyState_Chase(Enemy_AiCtrl controller)
    {
        this.enemyAiCtrl = controller;
    }

    public EnemyStateId GetId()
    {
        return EnemyStateId.Chase;
    }

    public void Enter()
    {

    }

    public void Update()
    {
        //if (this.enemyAiCtrl.EnemyCtrl.EnemyHealth.IsDeath())
        //{
        //    this.enemyAiCtrl.EnemySM.ChangeState(EnemyStateId.Death);
        //}

        if (this.enemyAiCtrl.EnemyCtrl.DetectTarget.IsDetectTarget())
        {
            Vector3 targetPosition = this.enemyAiCtrl.EnemyCtrl.DetectTarget.FindClosest(FactionType.Alliance).GetCenterPoint().position;
            NavMeshAgent navMeshAgent = this.enemyAiCtrl.EnemyCtrl.NavMeshAgent;
            this.timer -= Time.deltaTime;

            if (this.timer < 0)
            {
                Vector3 direction = targetPosition - this.enemyAiCtrl.EnemyCtrl.CenterPoint.position;
                direction.y = 0;
                if (direction.sqrMagnitude > this.enemyAiCtrl.AiConfig.MaxDistance * this.enemyAiCtrl.AiConfig.MaxDistance)
                {
                    navMeshAgent.destination = targetPosition;
                }
                this.timer = this.enemyAiCtrl.AiConfig.MaxDistance;
            }
        }
    }

    public void FixedUpdate()
    {
        if (!this.enemyAiCtrl.EnemyCtrl.NavMeshAgent.enabled) return;

    }

    public void Exit()
    {

    }
}
