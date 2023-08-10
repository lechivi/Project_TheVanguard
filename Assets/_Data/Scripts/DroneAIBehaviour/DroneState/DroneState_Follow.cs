using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DroneState_Follow : IDroneState
{
    private Drone_AiCtrl droneAiCtrl;

    private ParticleSystem moveFx;

    public DroneState_Follow(Drone_AiCtrl controller)
    {
        this.droneAiCtrl = controller;

        ParticleSystem movementFx = this.droneAiCtrl.DroneCtrl.MoveFx;
        if (movementFx != null)
        {
            this.moveFx = movementFx;
        }
    }

    public DroneStateId GetId()
    {
        return DroneStateId.Follow;
    }

    public void Enter()
    {
        if (this.moveFx != null && !this.moveFx.isPlaying)
            this.moveFx.Play();
    }

    public void Exit()
    {

    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        this.Follow();
    }

    private void Follow()
    {
        DroneCtrl droneCtrl = this.droneAiCtrl.DroneCtrl;
        if (droneCtrl.TargetFollow != null)
        {
            droneCtrl.Agent.SetDestination(droneCtrl.TargetFollow.transform.position);

            droneCtrl.transform.LookAt(droneCtrl.TargetFollow.transform.position);

            if (droneCtrl.Agent.remainingDistance > 5)
            {
                droneCtrl.Agent.speed = 7;
            }
            else
            {
                droneCtrl.Agent.speed = 3;
            }
        }
    }
}
