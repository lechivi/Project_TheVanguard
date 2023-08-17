using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : SaiMonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private DetectPlayer detectPlayer;
    [SerializeField] private Enemy_AiCtrl enemyAiCtrl;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private EnemyDebuffs enemyDebuffs;

    public Animator Animator { get => this.animator; }
    public Transform CenterPoint { get => this.centerPoint; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.animator == null)
            this.animator = GetComponent<Animator>();

        if (this.agent == null)
            this.agent = GetComponent<NavMeshAgent>();

        if (this.centerPoint == null)
            this.centerPoint = transform.Find("CenterPoint");

        if (this.detectPlayer == null)
            this.detectPlayer = GetComponentInChildren<DetectPlayer>();

        if (this.enemyAiCtrl == null)
            this.enemyAiCtrl = GetComponentInChildren<Enemy_AiCtrl>();

        if (this.enemyHealth == null)
            this.enemyHealth = GetComponentInChildren<EnemyHealth>();

        if (this.enemyDebuffs == null)
            this.enemyDebuffs = GetComponentInChildren<EnemyDebuffs>();
    }


}
