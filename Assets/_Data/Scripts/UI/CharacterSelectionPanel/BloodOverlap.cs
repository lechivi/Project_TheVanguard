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
        if (PlayerCtrl.HasInstance)
        {
            PlayerCtrl.Instance.PlayerHealth.OnTakeDamage += TriggerHit;
            Debug.Log("Dk event");
        }
    }

    private void OnEnable()
    {
        this.canvasGroup.alpha = 0f;
    }

    bool lowHP = true;

    private void FixedUpdate()
    {
        if (PlayerCtrl.HasInstance)
        {
            PlayerHealth playerHealth = PlayerCtrl.Instance.PlayerHealth;
            if (playerHealth.IsDeath())
            {
                if (this.canvasGroup.alpha != 1)
                    this.canvasGroup.alpha = 1;
            }
            else
            {
                //int curHHealth = playerHealth.GetCurrentHealth();
                //int maxHealth = playerHealth.GetMaxHealth();

                //if (curHHealth <= (int)maxHealth / 10)
                //{
                //    if (this.lowHP)
                //    {
                //        this.animator.SetBool("LowHP", true);
                //        this.lowHP = false;
                //    }
                //}
                //else
                //{
                //    if (!this.lowHP)
                //    {
                //        this.animator.SetBool("LowHP", false);
                //        this.lowHP = true;
                //    }

                //}
                //this.animator.SetBool("LowHP", curHHealth <= (int)maxHealth / 10 ? true : false);
            }
        }
    }

    private void OnDestroy()
    {
        if (PlayerCtrl.HasInstance)
        {
            PlayerCtrl.Instance.PlayerHealth.OnTakeDamage -= TriggerHit;
        }
    }

    private void TriggerHit()
    {
        Debug.Log("Hit");
        this.animator.SetTrigger("Hit");
    }
}
