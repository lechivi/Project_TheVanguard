using UnityEngine;
using UnityEngine.AI;

public class EnemyState_Chase : MonoBehaviour, IEnemyState
{
    private Enemy_AiCtrl enemyAiCtrl;
    private float attackCD = 1.5f;
    private float attackRange = 2;
    private float timer = 0;
    private float timePassed = 0;

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
        this.timePassed = this.attackCD;
    }

    public void Update()
    {
        //if (this.enemyAiCtrl.EnemyCtrl.EnemyHealth.IsDeath())
        //{
        //    this.enemyAiCtrl.EnemySM.ChangeState(EnemyStateId.Death);
        //}

        if (this.enemyAiCtrl.EnemyCtrl.DetectTarget.IsDetectTarget() == false) return;
        Transform target = this.enemyAiCtrl.EnemyCtrl.DetectTarget.FindClosest(FactionType.Alliance).GetTransform();
        NavMeshAgent navMeshAgent = this.enemyAiCtrl.EnemyCtrl.NavMeshAgent;
        this.timer -= Time.deltaTime;

        if (this.timer < 0)
        {
            Vector3 direction = target.position - this.enemyAiCtrl.EnemyCtrl.transform.position;
            direction.y = 0;
            if (direction.sqrMagnitude > this.enemyAiCtrl.AiConfig.MaxDistance * this.enemyAiCtrl.AiConfig.MaxDistance)
            {
                navMeshAgent.destination = target.position;
            }
            this.timer = this.enemyAiCtrl.AiConfig.MaxDistance;
        }

        this.enemyAiCtrl.EnemyCtrl.transform.LookAt(new Vector3(target.position.x, this.enemyAiCtrl.EnemyCtrl.transform.position.y, target.position.z));
        this.timePassed += Time.deltaTime;
        if (this.timePassed >= this.attackCD)
        {
            if (Vector3.Distance(target.position, this.enemyAiCtrl.EnemyCtrl.transform.position) <= this.attackRange)
            {
                this.enemyAiCtrl.EnemyCtrl.Animator.SetTrigger(Random.Range(0, 2) == 0 ? "Attack1" : "Attack2");
                this.timePassed = 0;
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
