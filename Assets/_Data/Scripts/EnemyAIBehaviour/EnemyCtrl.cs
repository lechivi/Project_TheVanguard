using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : SaiMonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private TakeDamageCtrl takeDamageCtrl;
    [SerializeField] private GraphicEffect graphicEffect;
    [SerializeField] private DetectTarget detectTarget;
    [SerializeField] private Enemy_AiCtrl enemyAiCtrl;
    [SerializeField] private EnemyLocomotion enemyLocomotion;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private EnemyDebuffs enemyDebuffs;
    [SerializeField] private RagdollCtrl ragdollCtrl;

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

        if (this.takeDamageCtrl == null)
        {
            this.takeDamageCtrl = GetComponent<TakeDamageCtrl>();
            this.takeDamageCtrl.SetHealthObject(this.enemyHealth.gameObject);
        }
    }
}