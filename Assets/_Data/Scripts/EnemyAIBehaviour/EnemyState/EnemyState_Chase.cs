using UnityEngine;

public class EnemyState_Chase : IEnemyState
{
    private Enemy_AiCtrl enemyAiCtrl;

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
