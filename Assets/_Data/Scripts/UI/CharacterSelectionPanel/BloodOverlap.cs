using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodOverlap : BaseUIElement
{
    [Header("BLOOD OVERLAY")]
    [SerializeField] private Animator animator;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.animator == null)
            this.animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UpdatePlayerHealth, this.OnPlayerHealthChanged);
        }
    }

    private void OnEnable()
    {
        this.canvasGroup.alpha = 0f;
    }

    bool lowHP = true;

    private void OnDestroy()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UpdatePlayerHealth, this.OnPlayerHealthChanged);
        }
    }

    private void OnPlayerHealthChanged(object value)
    {
        if (value == null) return;
        if (value is PlayerHealth playerHealth)
        {
            int cur = playerHealth.GetCurrentHealth();
            int max = playerHealth.GetMaxHealth();

            if (cur < max)
            {
                if (cur <= (int)max / 10)
                {
                    if (this.animator.GetBool("LowHP") == false)
                        this.animator.SetBool("LowHP", true);
                }
                else
                {
                    this.animator.SetBool("LowHP", false);
                    this.animator.SetTrigger("Hit");
                }
            }
        }
    }
}
