using UnityEngine;

public class Enemy_AiCtrl : EnemyAbstract
{
    public EnemyStateId InitState;
    public EnemyStateMachine EnemySM;

    private void Start()
    {
        this.EnemySM = new EnemyStateMachine(this);
        this.EnemySM.RegisterState(new EnemyState_Idle(this));
        this.EnemySM.RegisterState(new EnemyState_Chase(this));
    }

    private void Update()
    {
        this.EnemySM.Update();
    }

    private void FixedUpdate()
    {
        //Debug.Log("CurState: " + this.EnemySM.CurrentState);
        this.EnemySM.FixedUpdate();
    }
}
