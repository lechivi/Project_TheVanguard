using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : SaiMonoBehaviour
{
    [Header("CHARACTER")]
    [SerializeField] protected Transform characterTransform;
    [SerializeField] protected CharacterController characterController;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Animator rigAnimator;
    [SerializeField] protected Transform tps_LookAt;
    [SerializeField] protected Transform fps_Follow;

    public Transform CharacterTransform { get => this.characterTransform; }
    public CharacterController CharacterController { get => this.characterController; }
    public Animator Animator { get => this.animator; }
    public Animator RigAnimator { get => this.rigAnimator; }
    public Transform TPS_LookAt { get => this.tps_LookAt; }
    public Transform FPS_Follow { get => this.fps_Follow; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterTransform();
        this.LoadCharacterController();
        this.LoadAnimator();
        this.LoadRigAnimator();
        this.LoadTPSLookAt();
        this.LoadFPSFollow();
    }
    protected virtual void LoadCharacterTransform()
    {
        if (this.characterTransform == null)
        {
            this.characterTransform = transform;
            Debug.LogWarning(gameObject.name + ": LoadCharacterTransform", gameObject);
        }
    }   
    protected virtual void LoadCharacterController()
    {
        if (this.characterController == null)
        {
            this.characterController = GetComponent<CharacterController>();
            Debug.LogWarning(gameObject.name + ": LoadCharacterController", gameObject);
        }
    }
    protected virtual void LoadAnimator()
    {
        if (this.animator == null)
        {
            this.animator = GetComponent<Animator>();
            Debug.LogWarning(gameObject.name + ": LoadAnimator", gameObject);
        }
    }
    protected virtual void LoadRigAnimator()
    {
        if (this.rigAnimator == null)
        {
            this.rigAnimator = transform.Find("------RigLayers-----").GetComponent<Animator>();
            Debug.LogWarning(gameObject.name + ": LoadRigAnimator", gameObject);
        }
    }   
    protected virtual void LoadTPSLookAt()
    {
        if (this.tps_LookAt == null)
        {
            this.tps_LookAt = transform.Find("TPS_LookAt");
            Debug.LogWarning(gameObject.name + ": LoadTPSLookAt", gameObject);
        }
    }   
    protected virtual void LoadFPSFollow()
    {
        if (this.fps_Follow == null)
        {
            this.fps_Follow = transform.Find("------RigLayers-----").Find("WeaponHolder").Find("FPS_Follow");
            Debug.LogWarning(gameObject.name + ": LoadFPSFollow", gameObject);
        }
    }

    public virtual void ActionMouseL()
    {
        //for overrite
    }

    public virtual void ActionMouseR()
    {
        //for overrite
    }

    public virtual void SpecialSkill()
    {
        //for overrite
    }

    public virtual void BattleSkill()
    {
        //for overrite
    }
}
