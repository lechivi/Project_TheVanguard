using UnityEngine;

public class EnemyState_Idle : IEnemyState
{
    private Enemy_AiCtrl enemyAiCtrl;
    private float maxSightDistance;

    public EnemyState_Idle(Enemy_AiCtrl controller)
    {
        this.enemyAiCtrl = controller;
    }

    public EnemyStateId GetId()
    {
        return EnemyStateId.Idle;
    }

    public void Enter()
    {
        
    }

    public void Update()
    {
        DetectTarget detectTarget = this.enemyAiCtrl.EnemyCtrl.DetectTarget;
        if (detectTarget.IsDetectTarget() && this.enemyAiCtrl.EnemyCtrl.Target != null)
        {
            Vector3 targetDirection = this.enemyAiCtrl.EnemyCtrl.Target.position - this.enemyAiCtrl.EnemyCtrl.transform.position;
            targetDirection.Normalize();
            Vector3 transformDirection = this.enemyAiCtrl.EnemyCtrl.transform.forward;

            float dotProduct = Vector3.Dot(targetDirection, transformDirection);
            if (dotProduct >= 0)
            {
                this.enemyAiCtrl.EnemySM.ChangeState(EnemyStateId.Chase);
            }
        }
        else
        {

        }

    }

    public void FixedUpdate()
    {
        
    }

    public void Exit()
    {
        
    }
}
