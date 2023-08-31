using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : SaiMonoBehaviour
{
    [Header("CHARACTER")]
    [SerializeField] protected CharacterDataSO characterData;
    [SerializeField] protected Transform characterTransform;
    [SerializeField] protected CharacterController characterController;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Animator rigAnimator;
    [SerializeField] protected Transform tps_LookAt;
    [SerializeField] protected Transform fps_Follow;
    [SerializeField] protected Transform[] weaponSheathSlots;

    [Space(10)]
    [SerializeField] protected float cooldownSpecialSkill;
    [SerializeField] protected float cooldownBattleSkill;

    protected bool isReadySpecialSkill = true;
    protected bool isCoolingDownSpecicalSkill;
    protected float timerSpecialSkill;

    protected bool isReadyBattleSkill = true;
    protected bool isStartCooldownBattleSkill;
    protected float timerBattleSkill;
    protected bool isCharacterForm =false;

    public CharacterDataSO CharacterData { get => this.characterData; }
    public Transform CharacterTransform { get => this.characterTransform; }
    public CharacterController CharacterController { get => this.characterController; }
    public Animator Animator { get => this.animator; }
    public Animator RigAnimator { get => this.rigAnimator; }
    public Transform TPS_LookAt { get => this.tps_LookAt; }
    public Transform FPS_Follow { get => this.fps_Follow; }
    public Transform[] WeaponSheathSlots { get => this.weaponSheathSlots; }

    public bool IsReadySpecialSkill { get => this.isReadySpecialSkill; }
    public bool IsCoolingDownSpecicalSkill { get => this.isCoolingDownSpecicalSkill; }
    public float TimerSpecialSkill { get => this.timerSpecialSkill; }
    public bool IsCharacterForm { get => this.isCharacterForm; }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterTransform();
        this.LoadCharacterController();
        this.LoadAnimator();
        this.LoadRigAnimator();
        this.LoadTPSLookAt();
        this.LoadFPSFollow();
        this.LoadWeaponSheathSlots();
    }

    protected virtual void LoadWeaponSheathSlots()
    {
        if (this.weaponSheathSlots.Length != 3)
        {
            this.weaponSheathSlots = new Transform[3];
            Transform rigLayer_WeaponAim = this.rigAnimator.transform.Find("RigLayer_WeaponAim");
            this.weaponSheathSlots[0] = rigLayer_WeaponAim.Find("WeaponSlotLeft_Contains");
            this.weaponSheathSlots[1] = rigLayer_WeaponAim.Find("WeaponSlotRight_Contains");
            this.weaponSheathSlots[2] = rigLayer_WeaponAim.Find("WeaponSlotBack_Contains");
        }
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

    private void Start()
    {
        if (this.characterData != null)
        {
            this.cooldownSpecialSkill = this.characterData.CooldownSkillTime;
        }
    }

    protected virtual void Update()
    {
        if (this.isCoolingDownSpecicalSkill)
        {
            this.CooldownSpecialSkill();
        }
        if (this.isStartCooldownBattleSkill)
        {
            this.CooldownBattleSkill();
        }
    }

    public virtual void ActionMouseL()
    {
        //for overrite
    }

    public virtual void ActionMouseR(bool inputButton)
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

    public virtual void SetActiveCharacter()
    {
        PlayerCtrl.Instance.SetCharacter(this);
    }

    protected virtual void CooldownSpecialSkill()
    {
        this.timerSpecialSkill += Time.deltaTime;
        if (this.timerSpecialSkill < this.cooldownSpecialSkill) return;

        this.timerSpecialSkill = 0;
        this.isReadySpecialSkill = true;
        this.isCoolingDownSpecicalSkill = false;
    }

    protected virtual void CooldownBattleSkill()
    {
        this.timerBattleSkill += Time.deltaTime;
        if (this.timerBattleSkill < this.cooldownBattleSkill) return;

        this.timerBattleSkill = 0;
        this.isReadyBattleSkill = true;
        this.isStartCooldownBattleSkill = false;
    }
}
