using UnityEngine;

public class PlayerInput : PlayerAbstract
{
    public Vector2 MovementInput;
    public bool SprintInput;
    public bool WalkInput;
    public bool JumpInput;
    public bool CrounchInput;
    public bool ReloadInput;
    public bool InteractInput;
    public bool ChangeCameraInput;
    public bool AttackInput;
    public bool AimInput;
    public bool SpecialSkillInput;
    public bool BattleSkillInput;
    public bool HolsterInput;

    public bool MenuOpenCloseInput;

    public bool IsPlayerActive { get; private set; }

    private PlayerControls playerControls;

    private void OnEnable()
    {
        if (this.playerCtrl.Character == null) return;

        if (this.playerControls == null)
        {
            this.playerControls = new PlayerControls();
            this.playerControls.PlayerMovement.Movement.performed += i => this.MovementInput = i.ReadValue<Vector2>();

            this.playerControls.PlayerAction.Sprint.performed += i => this.SprintInput = true;
            this.playerControls.PlayerAction.Sprint.canceled += i => this.SprintInput = false;
            this.playerControls.PlayerAction.Walk.performed += i => this.WalkInput = true;
            this.playerControls.PlayerAction.Walk.canceled += i => this.WalkInput = false;

            this.playerControls.PlayerAction.Jump.performed += i => this.JumpInput = true;

            this.playerControls.PlayerAction.Attack.performed += i => this.AttackInput = true;
            this.playerControls.PlayerAction.Attack.canceled += i => this.AttackInput = false;

            this.playerControls.PlayerAction.Aim.started += i => this.playerCtrl.PlayerCamera.Check = true;
            this.playerControls.PlayerAction.Aim.performed += i => this.AimInput = true;
            this.playerControls.PlayerAction.Aim.canceled += i => this.AimInput = false;

            this.playerControls.PlayerAction.Reload.performed += i => this.ReloadInput = true;
            this.playerControls.PlayerAction.Interact.performed += i => this.InteractInput = true;
            this.playerControls.PlayerAction.ChangeCamera.performed += i => this.ChangeCameraInput = true;

            this.playerControls.PlayerAction.SpecialSkill.performed += i => this.SpecialSkillInput = true;
            this.playerControls.PlayerAction.BattleSkill.performed += i => this.BattleSkillInput = true;

            this.playerControls.UI.MenuOpenClose.performed += i => this.MenuOpenCloseInput = true;

            this.playerControls.PlayerAction.Holster.performed += i => this.HolsterInput = true;

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

    public void HandleUpdateAllInput()
    {
        this.HandleHolsterInput();
        this.HandleMovementInput();
        this.HandleSprintInput();
        this.HandleWalkInput();
        this.HandleCameraInput();
        this.HandleAttackInput();
        this.HandleReloadInput();
        this.HandleAimInput();
        this.HandleInteractInput();
        this.HandleSpecialSkillInput();
        this.HandleBattleSkillInput();
        this.HandleMenuOpenCloseInput();

        if (AimInput)
            Debug.Log("Aim");
    }

    private void HandleHolsterInput()
    {
        if (HolsterInput)
        {
            playerCtrl.PlayerWeapon.PlayerWeaponManager.SetHolster(HolsterInput);
            HolsterInput = false;
        }
    }
    private void HandleMovementInput()
    {
        float horizontalInput = this.MovementInput.x;
        float verticalInput = this.MovementInput.y;
        float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        this.playerCtrl.PlayerAnimation.UpdateAnimatorValuesMoveState(moveAmount, this.playerCtrl.PlayerLocomotion.IsSprinting, this.playerCtrl.PlayerLocomotion.IsWalking);
        this.playerCtrl.PlayerAnimation.UpdateValuesAnimation("InputX", horizontalInput);
        this.playerCtrl.PlayerAnimation.UpdateValuesAnimation("InputY", verticalInput);
    }

    private void HandleSprintInput()
    {
        playerCtrl.PlayerLocomotion.SetIsSprinting(SprintInput);
    }

    private void HandleWalkInput()
    {
        if(this.WalkInput)
        {
            playerCtrl.PlayerLocomotion.IsWalking = true;
        }
        else
        {
            playerCtrl.PlayerLocomotion.IsWalking = false;
        }
    }

    private void HandleAttackInput()
    {
        if (this.AttackInput)
        {
            this.playerCtrl.PlayerCombatAction.ActionMouseL();
        }
        else
        {
            WeaponRaycast gun = playerCtrl.PlayerWeapon.PlayerWeaponManager.GetActiveRaycastWeapon();
            if (gun != null)
            this.playerCtrl.PlayerWeapon.PlayerWeaponActive.IsFiring = false;
        }
    }

    private void HandleCameraInput()
    {

        if (ChangeCameraInput)
        {
            ChangeCameraInput = false;
            playerCtrl.PlayerCamera.ChangeOriginalCamera();
        }
    }

    private void HandleReloadInput()
    {
        if (ReloadInput)
        {
            playerCtrl.PlayerWeapon.PlayerWeaponReload.SetReloadWeapon(true);
            ReloadInput = false;
        }
    }

    private void HandleAimInput()
    {
        this.playerCtrl.PlayerCombatAction.ActionMouseR(true, AimInput);
       // playerCtrl.PlayerAim.SetIsAim(AimInput);
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
        if (UIManager.HasInstance)
        {
            if (this.MenuOpenCloseInput)
            {
                if (this.IsPlayerActive)
                {
                    UIManager.Instance.InGamePanel.ShowPauseMenu(null);
                }
                else
                {
                    UIManager.Instance.InGamePanel.ShowAlwaysOnUI(null);
                }
                this.SetPlayerInput(!this.IsPlayerActive);

                this.MenuOpenCloseInput = false;
            }
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
            this.playerCtrl.PlayerCamera.SetActiveCineCamera(true);
        }
        else
        {
            //Unlock & show cursor (PauseMenuCanvas enable)
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            this.playerControls.PlayerMovement.Disable();
            this.playerControls.PlayerAction.Disable();
            this.playerCtrl.PlayerCamera.SetActiveCineCamera(false);


            this.ResetInput();
        }

        this.IsPlayerActive = isActive;
    }

    public void ResetInput()
    {
        this.MovementInput = Vector2.zero;
    }


}
