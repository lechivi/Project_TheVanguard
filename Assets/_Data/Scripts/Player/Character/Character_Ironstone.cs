using System.Collections;
using UnityEngine;

public class Character_Ironstone : Character
{
    [Header("IRONSTONE")]

    [SerializeField] private ParticleSystem Particle;
    private AnimatorStateInfo state;
    public GameObject Hammer;
    private float timeDelta;
    [SerializeField] private float delayAttack;
    [SerializeField] private float transformTime;
    private bool canAttack;
    public float damageRange = 3.5f;

    protected override void Update()
    {
        base.Update();
        if (isReadySpecialSkill) return;
        this.HandleStateAnima();
        this.DelayAttack();
    }

    private void HandleStateAnima()
    {
        if (PlayerCtrl.Instance.PlayerInput.MovementInput != Vector2.zero)
        {
            Animator.SetLayerWeight(3, 0);
            Animator.SetLayerWeight(4, 1);
            state = Animator.GetCurrentAnimatorStateInfo(4);
        }
        else
        {
            Debug.Log("Idle");
            Animator.SetLayerWeight(3, 1);
            Animator.SetLayerWeight(4, 0);
            state = Animator.GetCurrentAnimatorStateInfo(4);
            state = Animator.GetCurrentAnimatorStateInfo(3);

        }

        if (state.IsName("HammerForm"))
        {
            if (state.normalizedTime > 0.5)
            {
                Hammer.gameObject.SetActive(true);
            }
        }

        if (state.IsName("Attack"))
        {
            if (state.normalizedTime > 0.5)
            {
                if (Particle.isPlaying)
                {
                    Debug.Log("Particle"); return;
                }
                Particle.Play();
            }
            PlayerCtrl.Instance.PlayerLocomotion.velocity.y -= 25 * Time.deltaTime;
        }
        else
        {
            Particle.Stop();
        }

    }

    public override void ActionMouseL()
    {
        base.ActionMouseL();
        if (canAttack)
        {
            this.Attack();
        }
    }

    public void DelayAttack()
    {
        timeDelta += Time.deltaTime;
        if (timeDelta < delayAttack) return;
        timeDelta = 0;
        canAttack = true;
    }
    public void Attack()
    {
        Animator.SetTrigger("AttackHammer");
        canAttack = false;
        NovaImpactDamage();
    }

    public void NovaImpactDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<ColliderHit>())
            {
                collider.GetComponent<ColliderHit>().Hit();
            }
        }
    }
    public override void SpecialSkill()
    {
        base.SpecialSkill();
        if (this.isReadySpecialSkill)
        {
            StartCoroutine(TransformationCoroutine());
        }
    }

    private IEnumerator TransformationCoroutine()
    {
        this.Transform();
        yield return new WaitForSeconds(this.transformTime);
        this.RevertoForm();
        yield return new WaitForSeconds(1f);
        this.Animator.SetBool("RevertoForm", false);
    }

    public void Transform()
    {
        this.isSpecialSkill = true;
        this.isReadySpecialSkill = false;
        PlayerCtrl.Instance.PlayerCombatAction.SetActionMouseLeft(CombatAction.CharacterSpecific);
        this.Animator.SetTrigger("HammerTransfer");
    }
    public void RevertoForm()
    {
        this.isSpecialSkill = false;
        isCoolingDownSpecicalSkill = true;
        this.Animator.SetBool("RevertoForm", true);
        PlayerCtrl.Instance.PlayerCombatAction.SetActionMouseLeft(CombatAction.None);
        Hammer.gameObject.SetActive(false);
    }
}
