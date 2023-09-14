using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Death : IEnemyState
{
    private Enemy_AiCtrl enemyAiCtrl;

    public EnemyState_Death(Enemy_AiCtrl controller)
    {
        this.enemyAiCtrl = controller;
    }

    public EnemyStateId GetId()
    {
        return EnemyStateId.Death;
    }

    public void Enter()
    {
        //this.enemyAiCtrl.EnemyCtrl.NavMeshAgent.enabled = false;
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    public void Exit()
    {
        
    }

}
