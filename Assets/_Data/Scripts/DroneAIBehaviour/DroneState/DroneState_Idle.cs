using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneState_Idle : IDroneState
{
    private Drone_AiCtrl droneAiCtrl;

    private ParticleSystem moveFx;

    public DroneState_Idle(Drone_AiCtrl controller)
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
        return DroneStateId.Idle;
    }

    public void Enter()
    {
        if (this.moveFx != null/* && !this.moveFx.isPlaying*/)
        {
            this.moveFx.Stop();
            Debug.Log("EnterState: Idle");
        }
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
