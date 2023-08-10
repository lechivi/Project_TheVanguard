using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneCtrl : SaiMonoBehaviour
{
    [Header("REFERENCE")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private FloatingObject floatingObj;
    [SerializeField] private ParticleSystem timeoutFx;
    [SerializeField] private ParticleSystem moveFx;
    [SerializeField] private List<ParticleSystem> listSwingFx;
    [SerializeField] private Drone_AiCtrl droneAiCtrl;

    [Space(10)]
    [SerializeField] private Transform targetFollow;
    [SerializeField] private float lifeTime = 30f;

    public NavMeshAgent Agent { get => this.agent; }
    public Transform TargetFollow { get => this.targetFollow; }
    public ParticleSystem MoveFx { get => this.moveFx; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.floatingObj == null)
            this.floatingObj = GetComponentInChildren<FloatingObject>();

        Transform fx = transform.Find("GFX").Find("FX");
        ParticleSystem[] fxArray = fx.GetComponentsInChildren<ParticleSystem>();
        if (this.moveFx == null)
            this.moveFx = fx.Find("FX_Flame_Booster_Round").GetComponent<ParticleSystem>();

        if (this.listSwingFx.Count != fxArray.Length - 1)
        {
            for (int i = 0; i < fxArray.Length; i++)
            {
                if (fxArray[i] != this.moveFx && !this.listSwingFx.Contains(fxArray[i]))
                {
                    this.listSwingFx.Add(fxArray[i]);
                }
            }
        }

        if (this.agent == null)
            this.agent = GetComponent<NavMeshAgent>();

        if (this.droneAiCtrl == null)
            this.droneAiCtrl = GetComponentInChildren<Drone_AiCtrl>();
    }

    //private void FixedUpdate()
    //{
    //    this.FollowTarget();
    //    this.SetActiveMovingFX();
    //}

    //private void FollowTarget()
    //{
    //    if (this.targetFollow != null)
    //    {
    //        this.agent.SetDestination(this.targetFollow.transform.position);

    //        transform.LookAt(this.targetFollow.transform.position);


    //        if (this.agent.remainingDistance > 5)
    //        {
    //            this.agent.speed = 6;
    //        }
    //        else
    //        {
    //            this.agent.speed = 3;
    //        }
    //    }
    //}

    //private void SetActiveMovingFX()
    //{
    //    if (this.agent.velocity.magnitude <= 0)
    //    {
    //        if (this.moveFx.gameObject.activeSelf == false) return;
    //        this.moveFx.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        if (this.moveFx.gameObject.activeSelf == true) return;
    //        this.moveFx.gameObject.SetActive(true);
    //    }
    //}

    private void Update()
    {
        Vector2 transformPosV2 = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPosV2 = new Vector2(this.TargetFollow.transform.position.x, this.TargetFollow.transform.position.z);
        float distance = Vector2.Distance(transformPosV2, targetPosV2);
        if (distance < 0.1f)
        {
            this.droneAiCtrl.DroneSM.ChangeState(DroneStateId.Idle);
        }
        else
        {
            this.droneAiCtrl.DroneSM.ChangeState(DroneStateId.Follow);
        }
    }

    private IEnumerator LifeTimeOfDrone()
    {
        float fxTime = 1.05f;
        yield return new WaitForSeconds(this.lifeTime - fxTime);

        this.timeoutFx.Play();
        yield return new WaitForSeconds(fxTime);

        gameObject.SetActive(false);
    }

    public void SetupDrone(Transform targetFollow, float lifeTime)
    {
        this.targetFollow = targetFollow;
        this.lifeTime = lifeTime;
     
        StartCoroutine(this.LifeTimeOfDrone());
    }
}
