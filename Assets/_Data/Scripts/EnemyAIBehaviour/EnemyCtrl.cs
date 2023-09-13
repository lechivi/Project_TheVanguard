using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : SaiMonoBehaviour
{
    [SerializeField] private EnemyDataSO enemyData;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private EnemyDealDamageCtrl enemyDealDamageCtrl;
    [SerializeField] private TakeDamageCtrl takeDamageCtrl;
    [SerializeField] private GraphicEffect graphicEffect;
    [SerializeField] private DetectTarget detectTarget;
    [SerializeField] private Enemy_AiCtrl enemyAiCtrl;
    [SerializeField] private EnemyLocomotion enemyLocomotion;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private EnemyDebuffs enemyDebuffs;
    [SerializeField] private RagdollCtrl ragdollCtrl;

    public IInfoScanner CurInfoScanTarget;
    public Transform Target;    //For LookAt
    public Vector3 FollowPos;   //For SetDestination NavMeshAgent

    public EnemyDataSO EnemyData { get => this.enemyData; }
    public Animator Animator { get => this.animator; }
    public CharacterController CharacterController { get => this.characterController; }
    public NavMeshAgent NavMeshAgent { get => this.navMeshAgent; }
    public Transform CenterPoint { get => this.centerPoint; }
    public GraphicEffect GraphicEffect { get => this.graphicEffect; }
    public DetectTarget DetectTarget { get => this.detectTarget; }
    public Enemy_AiCtrl EnemyAiCtrl { get => this.enemyAiCtrl; }
    public EnemyLocomotion EnemyLocomotion { get => this.enemyLocomotion; }
    public EnemyHealth EnemyHealth { get => this.enemyHealth; }
    public EnemyDebuffs EnemyDebuffs { get => this.enemyDebuffs; }
    public RagdollCtrl EnemyRagdoll { get => this.ragdollCtrl; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.animator == null)
            this.animator = GetComponent<Animator>();

        if (this.characterController == null)
            this.characterController = GetComponent<CharacterController>();

        if (this.navMeshAgent == null)
            this.navMeshAgent = GetComponent<NavMeshAgent>();

        if (this.centerPoint == null)
            //this.centerPoint = transform.Find("CenterPoint");
            this.centerPoint = transform.Find("Root/Hips/Spine_01/Spine_02");

        if (this.graphicEffect == null)
            this.graphicEffect = GetComponentInChildren<GraphicEffect>();
        if (this.graphicEffect != null)
        {
            if (this.graphicEffect.SkinnedMeshRenderer == null)
                this.graphicEffect.SkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
            if (this.graphicEffect.ListMeshRenderer.Count != meshRenderers.Length)
            {
                for (int i = 0; i < meshRenderers.Length; i++)
                {
                    this.graphicEffect.ListMeshRenderer.Add(meshRenderers[i]);
                }
            }
        }

        if (this.detectTarget == null)
            this.detectTarget = GetComponentInChildren<DetectTarget>();

        if (this.enemyAiCtrl == null)
            this.enemyAiCtrl = GetComponentInChildren<Enemy_AiCtrl>();

        if (this.enemyLocomotion == null)
            this.enemyLocomotion = GetComponentInChildren<EnemyLocomotion>();

        if (this.enemyHealth == null)
            this.enemyHealth = GetComponentInChildren<EnemyHealth>();

        if (this.enemyDebuffs == null)
            this.enemyDebuffs = GetComponentInChildren<EnemyDebuffs>();

        if (this.ragdollCtrl == null)
        {
            this.ragdollCtrl = GetComponentInChildren<RagdollCtrl>();
            this.ragdollCtrl.Animator = this.animator;
            this.ragdollCtrl.CharacterController = this.characterController;
        }

        if (this.enemyDealDamageCtrl == null)
            this.enemyDealDamageCtrl = GetComponent<EnemyDealDamageCtrl>();

        if (this.takeDamageCtrl == null)
        {
            this.takeDamageCtrl = GetComponent<TakeDamageCtrl>();
            this.takeDamageCtrl.SetHealthObject(this.enemyHealth.gameObject);
        }
    }

    private void Start()
    {
        if (this.enemyData != null)
        {
            transform.gameObject.name = this.enemyData.EnemyName;
            this.enemyHealth.SetHealth(this.EnemyData.Health);
            this.enemyDealDamageCtrl.DealDamageBox.Damage = this.EnemyData.Damage;
            this.navMeshAgent.speed = this.EnemyData.Speed;
            this.detectTarget.DetectionRange = this.enemyData.DetectionRange;
        }
    }

    private void Update()
    {
        IInfoScanner infoScanner = this.detectTarget.FindClosest(FactionType.Alliance);
        if (infoScanner != null)
        {
            if (this.CurInfoScanTarget != infoScanner)
            {
                this.CurInfoScanTarget = infoScanner;
            }

            if (infoScanner is AlliancePlayer_InfoScanner)
            {
                this.CurInfoScanTarget.GetTransform().GetComponent<Character>().LeadTracker.Add(this);
            }
            if (infoScanner is AllianceCompanion_InfoScanner)
            {
                this.CurInfoScanTarget.GetTransform().GetComponent<LeadTracker>().Add(this);
            }
        }
        else
        {
            if (this.CurInfoScanTarget != null)
            {
                if (infoScanner is AlliancePlayer_InfoScanner)
                {
                    this.CurInfoScanTarget.GetTransform().GetComponent<Character>().LeadTracker.Remove(this);
                }
                if (infoScanner is AllianceCompanion_InfoScanner)
                {
                    this.CurInfoScanTarget.GetTransform().GetComponent<LeadTracker>().Remove(this);
                }
                this.CurInfoScanTarget = null;
            }
        }

        if (this.Target != null && this.checkDetect)
        {
            this.checkDetect = false;
            this.animator.SetTrigger(Random.Range(0, 2) == 0 ? "Detect1" : "Detect2");
            transform.LookAt(new Vector3(this.Target.position.x, transform.position.y, this.Target.position.z));
        }
        if (this.Target == null)
        {
            this.checkDetect = true;
        }
    }

    private bool checkDetect;

    public void DropWeapon()
    {
        Transform weapon = this.enemyDealDamageCtrl.DealDamageBox.transform;
        if (weapon)
        {
            this.enemyDealDamageCtrl.DealDamageBox.enabled = false;
            weapon.GetComponent<Collider>().enabled = true;
            weapon.GetComponent<Collider>().isTrigger = false;
            weapon.AddComponent<Rigidbody>();
            weapon.SetParent(null);
            weapon.gameObject.layer = LayerMask.NameToLayer("TerrainOnly");
        }
    }

    public void ResetEnemy()
    {
        if (!this.enemyHealth.IsDeath()) return;

        this.ragdollCtrl.DisableRagdoll();
        this.enemyHealth.ResetHealth();
        this.enemyDebuffs.ResetDebuffs();
        this.enemyDealDamageCtrl.DealDamageBox.ResetWeapon();
        this.enemyAiCtrl.EnemySM.ChangeState(EnemyStateId.Idle);
    }
}