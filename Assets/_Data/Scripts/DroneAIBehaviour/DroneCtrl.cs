using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneCtrl : SaiMonoBehaviour
{
    [Header("REFERENCE")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private FloatingObject floatingObj;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private ParticleSystem laserFx;
    [SerializeField] private ParticleSystem timeoutFx;
    [SerializeField] private ParticleSystem moveFx;
    [SerializeField] private List<ParticleSystem> listSwingFx;
    [SerializeField] private DetectTarget detectTarget;
    [SerializeField] private Drone_AiCtrl droneAiCtrl;
    [SerializeField] private DroneHealth droneHealth;

    [Space(10)]
    [SerializeField] private Transform targetFollow;
    [SerializeField] private float lifeTime = 30f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float maxDistanceFromPlayer = 10f;
    [SerializeField] private float maxDistanceFromEnemy = 5f;
    [SerializeField] private float safeRange = 15f;

    public NavMeshAgent Agent { get => this.agent; }
    public Transform TargetFollow { get => this.targetFollow; }
    public ParticleSystem LaserFx { get => this.laserFx; }
    public ParticleSystem MoveFx { get => this.moveFx; }
    public DetectTarget DetectTarget { get => this.detectTarget; }
    public Drone_AiCtrl DroneAiCtrl { get => this.droneAiCtrl; }
    public DroneHealth DroneHealth { get => this.droneHealth; }
    public float RotationSpeed { get => this.rotationSpeed; }
    public float DetectionRange { get => this.detectionRange; }
    public float MaxDistanceFromPlayer { get => this.maxDistanceFromPlayer; }
    public float MaxDistanceFromEnemy { get => this.maxDistanceFromEnemy; }
    public float SafeRange { get => this.safeRange; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.floatingObj == null)
            this.floatingObj = GetComponentInChildren<FloatingObject>();

        if (this.shotPoint == null)
            this.shotPoint = transform.Find("GFX").Find("ShotPoint");

        if (this.laserFx == null)
            this.laserFx = this.shotPoint.GetComponentInChildren<ParticleSystem>();

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

        if (this.detectTarget == null)
            this.detectTarget = GetComponentInChildren<DetectTarget>();

        if (this.droneAiCtrl == null)
            this.droneAiCtrl = GetComponentInChildren<Drone_AiCtrl>();

        if (this.droneHealth == null)
            this.droneHealth = GetComponentInChildren<DroneHealth>();
    }

    private void OnEnable()
    {
        this.detectTarget.DetectionRange = this.detectionRange;
    }

    private void Update()
    {
        if (this.detectTarget.IsDetectTarget() 
            /*&& Vector3.Distance(this.targetFollow.position, this.detectTarget.FindClosest(FactionType.Voidspawn).GetCenterPoint().position) < this.safeRange*/)
        {
            this.droneAiCtrl.DroneSM.ChangeState(DroneStateId.Attack);
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(this.targetFollow.position, transform.position);

            if (distanceToPlayer < 0.1f)
            {
                this.droneAiCtrl.DroneSM.ChangeState(DroneStateId.Idle);
            }
            else
            {
                this.droneAiCtrl.DroneSM.ChangeState(DroneStateId.Follow);
            }

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

    public void ShotLaser()
    {
        this.shotPoint.LookAt(this.detectTarget.FindClosest(FactionType.Voidspawn).GetCenterPoint());
        this.laserFx.Play();
    }

    //private void OnDrawGizmos()
    //{
    //    if (this.detectEnemy.IsDetectEnemy())
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine(this.shotPoint.position, this.detectEnemy.FindClosest().CenterPoint.position);
    //    }
    //}
}
