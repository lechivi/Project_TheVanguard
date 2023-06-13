using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : SaiMonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerInput PlayerInput;
    public PlayerLocomotion PlayerLocomotion;
    public PlayerCamera PlayerCamera;
    public PlayerAnimation PlayerAnimation;

    public Transform PlayerTransform;
    public Transform CameraTransform;
    public Animator Animator;
    public CharacterController CharacterController;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerManager();
        this.LoadPlayerInput();
        this.LoadPlayerLocomotion();
        this.LoadPlayerCamera();
        this.LoadPlayerAnimation();

        this.LoadPlayerTransform();
        this.LoadCameraTransform();
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

    protected virtual void LoadPlayerTransform()
    {
        if (this.PlayerTransform == null)
        {
            this.PlayerTransform = transform.parent;
            Debug.LogWarning(gameObject.name + ": LoadPlayerTransform", gameObject);
        }
    }

    protected virtual void LoadCameraTransform()
    {
        if (this.CameraTransform == null)
        {
            this.CameraTransform = Camera.main.transform;
            Debug.LogWarning(gameObject.name + ": LoadCameraTransform", gameObject);
        }
    }

    protected virtual void LoadAnimator()
    {
        if (this.Animator == null)
        {
            this.Animator = transform.parent.GetComponent<Animator>();
            Debug.LogWarning(gameObject.name + ": LoadAnimator", gameObject);
        }
    }

    protected virtual void LoadCharacterController()
    {
        if (this.CharacterController == null)
        {
            this.CharacterController = transform.parent.GetComponent<CharacterController>();
            Debug.LogWarning(gameObject.name + ": LoadCharacterController", gameObject);
        }
    }
}
