using UnityEngine;

public class EnemyState_Idle : IEnemyState
{
    private Enemy_AiCtrl enemyAiCtrl;

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

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        
    }
}
