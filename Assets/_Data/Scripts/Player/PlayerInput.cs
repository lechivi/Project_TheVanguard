using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : PlayerAbstract
{
    public Vector2 MovementInput;
    public bool SprintInput;
    public bool JumpInput;
    public bool CrounchInput;
    public bool ReloadInput;
    public bool InteractInput;
    public bool ChangeCameraInput;
    public bool AttackInput;
    public bool AimInput;
    public bool SpecialSkillInput;
    public bool BattleSkillInput;

    public bool MenuOpenCloseInput;

    public bool IsPlayerActive { get; private set; }

    private PlayerControls playerControls;

    private void OnEnable()
    {
        if (this.playerControls == null)
        {
            this.playerControls = new PlayerControls();
            this.playerControls.PlayerMovement.Movement.performed += i => this.MovementInput = i.ReadValue<Vector2>();

            this.playerControls.PlayerAction.Sprint.performed += i => this.SprintInput = true;
            this.playerControls.PlayerAction.Sprint.canceled += i => this.SprintInput = false;

            this.playerControls.PlayerAction.Jump.performed += i => this.JumpInput = true;

            this.playerControls.PlayerAction.Attack.performed += i => this.AttackInput = true;
            this.playerControls.PlayerAction.Attack.canceled += i => this.AttackInput = false;

            this.playerControls.PlayerAction.Aim.performed += i => this.AimInput = true;
            this.playerControls.PlayerAction.Aim.canceled += i => this.AimInput = false;

            this.playerControls.PlayerAction.Reload.performed += i => this.ReloadInput = true;
            this.playerControls.PlayerAction.Interact.performed += i => this.InteractInput = true;
            this.playerControls.PlayerAction.ChangeCamera.performed += i => this.ChangeCameraInput = true;

            this.playerControls.PlayerAction.SpecialSkill.performed += i => this.SpecialSkillInput = true;
            this.playerControls.PlayerAction.BattleSkill.performed += i => this.BattleSkillInput = true;

            this.playerControls.UI.MenuOpenClose.performed += i => this.MenuOpenCloseInput = true;

            this.IsPlayerActive = true;
            this.SetPlayerInput(true);
        }

        this.playerControls.Enable();
    }

    private void OnDisable()
    {
        this.playerControls?.Disable();
        this.ResetInput();
    }

    public void HandleAllInput()
    {
        this.HandleMovementInput();
        this.HandleSprintInput();
        this.HandleCameraInput();
        this.HandleAttackInput();
        this.HandleReloadInput();
        this.HandleAimInput();
        this.HandleInteractInput();
        this.HandleSpecialSkillInput();
        this.HandleBattleSkillInput();
        this.HandleMenuOpenCloseInput();
    }

    private void HandleMovementInput()
    {
        float horizontalInput = this.MovementInput.x;
        float verticalInput = this.MovementInput.y;
        float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        this.playerCtrl.PlayerAnimation.UpdateAnimatorValuesMoveState(moveAmount, this.playerCtrl.PlayerLocomotion.IsSprinting);
        this.playerCtrl.PlayerAnimation.UpdateValuesAnimation("InputX", horizontalInput);
        this.playerCtrl.PlayerAnimation.UpdateValuesAnimation("InputY", verticalInput);
    }

    private void HandleSprintInput()
    {
        bool canSprint = this.SprintInput && this.MovementInput != Vector2.zero && !AttackInput && !playerCtrl.PlayerAim.isAim && !playerCtrl.PlayerWeapon.PlayerWeaponReload.isReload;
        if (canSprint)
        {
            this.playerCtrl.PlayerLocomotion.IsSprinting = true;
        }
        else
        {
            this.playerCtrl.PlayerLocomotion.IsSprinting = false;
        }
    }

    private void HandleAttackInput()
    {
        //if (playerCtrl.PlayerWeapon.PlayerWeaponActive == null) return;
        //if (this.AttackInput)
        //{
        //    playerCtrl.PlayerWeapon.PlayerWeaponActive.isFiring = true;
        //}
        //else if (!this.AttackInput)
        //{
        //    playerCtrl.PlayerWeapon.PlayerWeaponActive.isFiring = false;
        //}

        if (this.AttackInput)
        {
            this.playerCtrl.PlayerCombatAction.ActionMouseL();
            this.AttackInput = false;
        }
    }

    private void HandleCameraInput()
    {
        playerCtrl.PlayerCamera.HandleCameraOriginal();
        if (ChangeCameraInput)
        {
            ChangeCameraInput = false;
            playerCtrl.PlayerCamera.originalTPSCam = !playerCtrl.PlayerCamera.originalTPSCam;
        }
        if (playerCtrl.PlayerAim.isAim)
        {
            this.playerCtrl.PlayerCamera.ChangeFPSCam();
        }
        if (!playerCtrl.PlayerAim.isAim)
        {
            if (this.playerCtrl.PlayerCamera.originalTPSCam)
            {
                this.playerCtrl.PlayerCamera.ChangeTPSCam();
            }
            if (!this.playerCtrl.PlayerCamera.originalTPSCam)
            {
                this.playerCtrl.PlayerCamera.ChangeFPSCam();
            }

        }
    }

    private void HandleReloadInput()
    {
        RaycastWeapon weapon = playerCtrl.PlayerWeapon.PlayerWeaponManager.GetActiveRaycastWeapon();
        if (weapon)
        {
            if (ReloadInput || weapon.Weapon.WeaponData.Ammo <= 0)
            {
                playerCtrl.PlayerWeapon.PlayerWeaponReload.SetReloadWeapon();
                ReloadInput = false;
            }
        }
    }

    private void HandleAimInput()
    {
        RaycastWeapon weapon = playerCtrl.PlayerWeapon.PlayerWeaponManager.GetActiveRaycastWeapon();
        if (weapon)
        {
            if (AimInput && !playerCtrl.PlayerWeapon.PlayerWeaponActive.isHolster && !playerCtrl.PlayerWeapon.PlayerWeaponReload.isReload)
            {
                playerCtrl.PlayerAim.isAim = true;
            }
            else
            {
                playerCtrl.PlayerAim.isAim = false;
            }
        }
    }

    private void HandleInteractInput()
    {
        if (this.InteractInput)
        {
            this.playerCtrl.PlayerInteract.Interact();
            this.InteractInput = false;
        }
    }

    private void HandleSpecialSkillInput()
    {
        if (this.SpecialSkillInput)
        {
            this.playerCtrl.PlayerCombatAction.SpecialSkill();
            this.SpecialSkillInput = false;
        }
    }

    private void HandleBattleSkillInput()
    {
        if (this.BattleSkillInput)
        {
            this.playerCtrl.PlayerCombatAction.BattleSkill();
            this.BattleSkillInput = false;
        }
    }

    private void HandleMenuOpenCloseInput()
    {
        if (this.MenuOpenCloseInput)
        {
            if (this.IsPlayerActive)
            {
                UIManager.Instance.SetPauseMenuCanvasOpen();
            }
            else
            {
                UIManager.Instance.SetAlwaysOnUICanvasOpen();
            }
            this.SetPlayerInput(!this.IsPlayerActive);

            this.MenuOpenCloseInput = false;
        }
    }

    public void SetPlayerInput(bool isActive)
    {
        if (isActive)
        {
            //Lock & hide cursor (PauseMenuCanvas disable)
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            this.playerControls.PlayerMovement.Enable();
            this.playerControls.PlayerAction.Enable();
            this.playerCtrl.PlayerCamera.TPSCam.enabled = true;
            this.playerCtrl.PlayerCamera.FPSCam.enabled = true;
        }
        else
        {
            //Unlock & show cursor (PauseMenuCanvas enable)
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            this.playerControls.PlayerMovement.Disable();
            this.playerControls.PlayerAction.Disable();
            this.playerCtrl.PlayerCamera.TPSCam.enabled = false;
            this.playerCtrl.PlayerCamera.FPSCam.enabled = false;

            this.ResetInput();
        }

        this.IsPlayerActive = isActive;
    }

    public void ResetInput()
    {
        this.MovementInput = Vector2.zero;
    }


}
