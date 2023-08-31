using UnityEngine;

public class InputControls : SaiMonoBehaviour
{
    protected PlayerControls playerControls;

    protected virtual void OnEnable()
    {
        if (this.playerControls == null)
        {
            this.playerControls = new PlayerControls();
            this.SetupInput();
        }

        this.playerControls.Enable();
    }

    protected virtual void OnDisable()
    {
        this.playerControls?.Disable();
    }

    public virtual void SetupInput()
    {
        //for override
    }
}
