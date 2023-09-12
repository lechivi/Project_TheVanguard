using UnityEngine;
using UnityEngine.AI;

public class EnemyState_Chase : MonoBehaviour, IEnemyState
{
    private Enemy_AiCtrl enemyAiCtrl;
    private float attackCD = 1.5f;
    private float attackRange = 2;
    private float timer = 0;
    private float timePassed = 0;

    private bool useMovementPrediction = false;
    //private float movementPredictionThreshold = 0;
    //private float movementPredictionTime = 1;

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
        if (this.enemyAiCtrl.EnemyCtrl.CurTarget == null) return;
        Vector3 followPos = this.enemyAiCtrl.EnemyCtrl.FollowPos;
        this.FollowTarget(followPos);
        this.AttackTarget(followPos);
    }

    public void FixedUpdate()
    {
        if (!this.enemyAiCtrl.EnemyCtrl.NavMeshAgent.enabled) return;

    }

    public void Exit()
    {

    }

    private void FollowTarget(Vector3 followPos)
    {
        Transform target = this.enemyAiCtrl.EnemyCtrl.Target;
        this.enemyAiCtrl.EnemyCtrl.transform.LookAt(new Vector3(target.position.x, this.enemyAiCtrl.EnemyCtrl.transform.position.y, target.position.z));

        this.timer -= Time.deltaTime;
        if (this.timer < 0)
        {
            Vector3 direction = target.position - this.enemyAiCtrl.EnemyCtrl.transform.position;
            direction.y = 0;
            if (direction.sqrMagnitude > this.enemyAiCtrl.AiConfig.MaxDistance * this.enemyAiCtrl.AiConfig.MaxDistance)
            {
                this.enemyAiCtrl.EnemyCtrl.NavMeshAgent.SetDestination(followPos);
                //if (!this.useMovementPrediction)
                //    navMeshAgent.SetDestination(target.position);
                //else
                //    navMeshAgent.SetDestination(target.position + PlayerCtrl.Instance.PlayerLocomotion.AverageVelocity * this.movementPredictionTime);
            }
            this.timer = this.enemyAiCtrl.AiConfig.MaxDistance;
        }
    }

    private void AttackTarget(Vector3 followPos)
    {
        this.timePassed += Time.deltaTime;
        if (this.timePassed >= this.attackCD)
        {
            if (Vector3.Distance(followPos, this.enemyAiCtrl.EnemyCtrl.transform.position) <= this.attackRange)
            {
                this.enemyAiCtrl.EnemyCtrl.Animator.SetInteger("RandomAttack", Random.Range(0, 3));
                this.enemyAiCtrl.EnemyCtrl.Animator.SetTrigger("Attack");
                this.timePassed = 0;
            }
        }
    }
}
