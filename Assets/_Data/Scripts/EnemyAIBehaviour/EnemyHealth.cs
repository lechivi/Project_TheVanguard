using UnityEngine;

public class EnemyHealth : EnemyAbstract, IHealth
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth;

    private bool isDeath;

    private void Start()
    {
        this.currentHealth = this.maxHealth;
    }

    public int GetMaxHealth()
    {
        return this.maxHealth;
    }

    public int GetCurrentHealth()
    {
        return this.currentHealth;
    }

    public bool IsDeath()
    {
        return this.isDeath;
    }

    public void TakeDamage(int damage)
    {
        this.currentHealth -= damage;
        this.enemyCtrl.GraphicEffect.PlayHitEffect();
        if (this.currentHealth <= 0)
        {
            this.currentHealth = 0;
            this.Die();
        }
    }

    public void TakeDamage(int damage, Vector3 force, Vector3 hitPoint)
    {
        this.currentHealth -= damage;
        this.enemyCtrl.GraphicEffect.PlayHitEffect();
        if (this.currentHealth <= 0)
        {
            this.currentHealth = 0;
            this.Die(force, hitPoint);
        }
    }

    public void TakeDamage(int damage, Vector3 force, Vector3 hitPoint, Rigidbody hitRigidbody)
    {
        this.currentHealth -= damage;
        this.enemyCtrl.GraphicEffect.PlayHitEffect();
        if (this.currentHealth <= 0)
        {
            this.currentHealth = 0;
            this.Die(force, hitPoint, hitRigidbody);
        }
    }

    public void Die()
    {
        this.isDeath = true;
        this.enemyCtrl.EnemyAiCtrl.EnemySM.ChangeState(EnemyStateId.Death);
        this.enemyCtrl.EnemyRagdoll.EnableRagdoll();
    }

    public void Die(Vector3 force, Vector3 hitPoint)
    {
        this.isDeath = true;
        this.enemyCtrl.EnemyAiCtrl.EnemySM.ChangeState(EnemyStateId.Death);
        this.enemyCtrl.EnemyRagdoll.TriggerRagdoll(force, hitPoint);
    }

    public void Die(Vector3 force, Vector3 hitPoint, Rigidbody hitRigidbody)
    {
        this.isDeath = true;
        this.enemyCtrl.EnemyAiCtrl.EnemySM.ChangeState(EnemyStateId.Death);
        this.enemyCtrl.EnemyRagdoll.TriggerRagdoll(force, hitPoint, hitRigidbody);
    }
}
