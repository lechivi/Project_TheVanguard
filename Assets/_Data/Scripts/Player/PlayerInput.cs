using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : PlayerAbstract
{
    public Vector2 MovementInput;
    public bool SprintInput;
    public bool JumpInput;
    public bool AimInput;
    public bool AttackInput;
    public bool ChangeCameraInput;
    public bool ReloadInput;
    // public bool ShootInput;
    //  [SerializeField] private RaycastWeapon weapon;
    private PlayerControls playerControls;
    //private InputActionReference

    private void OnEnable()
    {
        if (this.playerControls == null)
        {
            this.playerControls = new PlayerControls();
            this.playerControls.PlayerMovement.Movement.performed += i => this.MovementInput = i.ReadValue<Vector2>();

            this.playerControls.PlayerAction.Sprint.performed += i => this.SprintInput = true;
            this.playerControls.PlayerAction.Sprint.canceled += i => this.SprintInput = false;

            this.playerControls.PlayerAction.Jump.performed += i => this.JumpInput = true;

            //this.playerControls.PlayerAction.Aim.performed += i => this.AimInput = true;
            //this.playerControls.PlayerAction.Aim.canceled += i => this.AimInput = false;

            this.playerControls.PlayerAction.Attack.performed += i => this.AttackInput = true;
            this.playerControls.PlayerAction.Attack.canceled += i => this.AttackInput = false;
            this.playerControls.PlayerAction.ChangeCamera.performed += i => this.ChangeCameraInput = true;
            this.playerControls.PlayerAction.Reload.performed += i => this.ReloadInput = true;

        }

        this.playerControls.Enable();
    }

    private void OnDisable()
    {
        this.playerControls?.Disable();
    }

    public void HandleAllInput()
    {
        this.HandleMovementInput();
        this.HandleSprintInput();
       // this.HandleJumpInput();
        this.HandleCameraInput();
        this.HandleAttackInput();
        this.HandleReloadInput();
    }

    private void HandleMovementInput()
    {
        float horizontalInput = this.MovementInput.x;
        float verticalInput = this.MovementInput.y;
        float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        
        this.playerCtrl.PlayerAnimation.UpdateAnimatorValuesMoveState(moveAmount, this.playerCtrl.PlayerLocomotion.IsSprinting);
        this.playerCtrl.PlayerAnimation.UpdateValuesAnimation("InputX", horizontalInput);
        this.playerCtrl.PlayerAnimation.UpdateValuesAnimation("InputY", verticalInput);
        //
        /*this.playerCtrl.Animator.SetFloat("InputX",horizontalInput, 0.1f, Time.deltaTime);
        this.playerCtrl.Animator.SetFloat("InputY", verticalInput, 0.1f, Time.deltaTime);*/
    }

    private void HandleSprintInput()
    {
        if (this.SprintInput && this.MovementInput != Vector2.zero)
        {
            this.playerCtrl.PlayerLocomotion.IsSprinting = true;
        }
        else
        {
            this.playerCtrl.PlayerLocomotion.IsSprinting = false;
        }
    }

    /*private void HandleJumpInput()
    {
        if (this.JumpInput)
        {
            this.JumpInput = false;
        }
    }*/

    private void HandleAttackInput()
    {
        if (this.AttackInput)
        {
            playerCtrl.PlayerWeapon.PlayerWeaponActive.isFiring = true;
        }
        else if (!this.AttackInput)
        {
            playerCtrl.PlayerWeapon.PlayerWeaponActive.isFiring = false;
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void HandleCameraInput()
    {
        if (ChangeCameraInput)
        {
            ChangeCameraInput = false;
            this.playerCtrl.PlayerCamera.ChangeCamera();
        }
    }

    private void HandleReloadInput()
    {
        RaycastWeapon weapon = playerCtrl.PlayerWeapon.PlayerWeaponActive.GetActiveWeapon();
        if (weapon)
        {
            if (ReloadInput || weapon.ammo <= 0)
            {
                playerCtrl.PlayerWeapon.PlayerWeaponReload.SetReloadWeapon();
                ReloadInput = false;
            }
        }
    }
}
