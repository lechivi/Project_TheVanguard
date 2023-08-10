using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : SaiMonoBehaviour
{
    public static PlayerCtrl Instance;

    public PlayerAim PlayerAim;
    public PlayerManager PlayerManager;
    public PlayerInput PlayerInput;
    public PlayerLocomotion PlayerLocomotion;
    public PlayerCamera PlayerCamera;
    public PlayerAnimation PlayerAnimation;
    public PlayerWeapon PlayerWeapon;
    public PlayerInteract PlayerInteract;
    public PlayerCombatAction PlayerCombatAction;
    public PlayerCharacter PlayerCharacter;
    public Transform MainCamera;

    [Header("TARGET PLAYER")]
    public Character Character;
    public Transform PlayerTransform;
    public CharacterController CharacterController;
    public Animator Animator;
    public Animator RigAnimator;

    [Header("UI")]
    public UI_Skill_Icon UI_Skill_Icon;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerAim();
        this.LoadPlayerManager();
        this.LoadPlayerInput();
        this.LoadPlayerLocomotion();
        this.LoadPlayerCamera();
        this.LoadPlayerAnimation();
        this.LoadPlayerWeapon();
        this.LoadPlayerInteract();
        this.LoadPlayerCombat();
        this.LoadPlayerCharacter();
        this.LoadMainCamera();

        this.LoadPlayerTransform();
        this.LoadCharacterController();
        this.LoadAnimator();
        this.LoadRigAnimator();
    }

    protected virtual void LoadPlayerAim()
    {
        if (this.PlayerAim == null)
        {
            this.PlayerAim = GetComponentInChildren<PlayerAim>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerAim", gameObject);
        }
    }

    protected virtual void LoadPlayerManager()
    {
        if (this.PlayerManager == null)
        {
            this.PlayerManager = GetComponentInChildren<PlayerManager>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerManager", gameObject);
        }
    }

    protected virtual void LoadPlayerInput()
    {
        if (this.PlayerInput == null)
        {
            this.PlayerInput = GetComponentInChildren<PlayerInput>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerInput", gameObject);
        }
    }

    protected virtual void LoadPlayerLocomotion()
    {
        if (this.PlayerLocomotion == null)
        {
            this.PlayerLocomotion = GetComponentInChildren<PlayerLocomotion>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerLocomotion", gameObject);
        }
    }

    protected virtual void LoadPlayerCamera()
    {
        if (this.PlayerCamera == null)
        {
            this.PlayerCamera = GetComponentInChildren<PlayerCamera>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerCamera", gameObject);
        }
    }

    protected virtual void LoadPlayerAnimation()
    {
        if (this.PlayerAnimation == null)
        {
            this.PlayerAnimation = GetComponentInChildren<PlayerAnimation>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerAnimation", gameObject);
        }
    }

    protected virtual void LoadPlayerWeapon()
    {
        if (this.PlayerWeapon == null)
        {
            this.PlayerWeapon = GetComponentInChildren<PlayerWeapon>();
            Debug.LogWarning(gameObject.name + ": LoadPLayerWeapon", gameObject);
        }
    }

    protected virtual void LoadPlayerInteract()
    {
        if (this.PlayerInteract == null)
        {
            this.PlayerInteract = GetComponentInChildren<PlayerInteract>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerInteract", gameObject);
        }
    }

    protected virtual void LoadPlayerCombat()
    {
        if (this.PlayerCombatAction == null)
        {
            this.PlayerCombatAction = GetComponentInChildren<PlayerCombatAction>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerCombat", gameObject);
        }
    }

    protected virtual void LoadPlayerCharacter()
    {
        if (this.PlayerCharacter == null)
        {
            this.PlayerCharacter = GetComponentInChildren<PlayerCharacter>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerCharacter", gameObject);
        }
    }

    protected virtual void LoadMainCamera()
    {
        if (this.MainCamera == null)
        {
            this.MainCamera = Camera.main.transform;
            Debug.LogWarning(gameObject.name + ": LoadCameraTransform", gameObject);
        }
    }

    protected virtual void LoadPlayerTransform()
    {
        if (this.Character != null)
        {
            this.PlayerTransform = this.Character.CharacterTransform;
            Debug.LogWarning(gameObject.name + ": LoadPlayerTransform", gameObject);
        }
    }

    protected virtual void LoadCharacterController()
    {
        if (this.Character != null)
        {
            this.CharacterController = this.Character.CharacterController;
            Debug.LogWarning(gameObject.name + ": LoadCharacterController", gameObject);
        }
    }

    protected virtual void LoadAnimator()
    {
        if (this.Character != null)
        {
            this.Animator = this.Character.Animator;
            Debug.LogWarning(gameObject.name + ": LoadAnimator", gameObject);
        }
    }

    protected virtual void LoadRigAnimator()
    {
        if (this.Character != null)
        {
            this.RigAnimator = this.Character.RigAnimator;
            Debug.LogWarning(gameObject.name + ": LoadRigAnimator", gameObject);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        PlayerCtrl.Instance = this;
    }

    private void Start()
    {
        this.SetUI();
    }

    public void SetCharacter(Character character)
    {
        this.Character = character;

        this.LoadPlayerTransform();
        this.LoadCharacterController();
        this.LoadAnimator();
        this.LoadRigAnimator();

        this.PlayerCamera.SetCameraTarget();
    }

    public void SetUI()
    {
        if (this.Character == null) return;

        this.UI_Skill_Icon.SetSkill(this.Character.CharacterData.SpecialSkillIcon);
    }
}
