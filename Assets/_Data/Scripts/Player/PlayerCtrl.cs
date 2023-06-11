using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : SaiMonoBehaviour
{
    [SerializeField] protected PlayerManager playerManager;
    [SerializeField] protected PlayerInput playerInput;
    [SerializeField] protected PlayerLocomotion playerLocomotion;
    [SerializeField] protected PlayerCamera playerCamera;
    [SerializeField] protected PlayerAnimation playerAnimation;

    [SerializeField] protected Animator animator;
    [SerializeField] protected CharacterController characterController;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerManager();
        this.LoadPlayerInput();
        this.LoadPlayerLocomotion();
        this.LoadPlayerCamera();
        this.LoadPlayerAnimation();

        this.LoadAnimator();
        this.LoadCharacterController();
    }

    protected virtual void LoadPlayerManager()
    {
        if (this.playerManager == null)
        {
            this.playerManager = GetComponentInChildren<PlayerManager>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerManager", gameObject);
        }
    }

    protected virtual void LoadPlayerInput()
    {
        if (this.playerInput == null)
        {
            this.playerInput = GetComponentInChildren<PlayerInput>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerInput", gameObject);
        }
    }

    protected virtual void LoadPlayerLocomotion()
    {
        if (this.playerLocomotion == null)
        {
            this.playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerLocomotion", gameObject);
        }
    }

    protected virtual void LoadPlayerCamera()
    {
        if (this.playerCamera == null)
        {
            this.playerCamera = GetComponentInChildren<PlayerCamera>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerCamera", gameObject);
        }
    }

    protected virtual void LoadPlayerAnimation()
    {
        if (this.playerAnimation == null)
        {
            this.playerAnimation = GetComponentInChildren<PlayerAnimation>();
            Debug.LogWarning(gameObject.name + ": LoadPlayerAnimation", gameObject);
        }
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator == null)
        {
            this.animator = transform.parent.GetComponent<Animator>();
            Debug.LogWarning(gameObject.name + ": LoadAnimator", gameObject);
        }
    }

    protected virtual void LoadCharacterController()
    {
        if (this.characterController == null)
        {
            this.characterController = transform.parent.GetComponent<CharacterController>();
            Debug.LogWarning(gameObject.name + ": LoadCharacterController", gameObject);
        }
    }
}
