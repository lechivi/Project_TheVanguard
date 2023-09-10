using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : PlayerAbstract, IHealth
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private bool isDeath;

    private void Start()
    {
        if (this.playerCtrl.Character)
        {
            this.maxHealth = this.playerCtrl.Character.CharacterData.Health;
            this.currentHealth = this.maxHealth;
        }
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
        if (this.currentHealth <= 0)
        {
            this.currentHealth = 0;
            this.isDeath = true;
            this.Die();
        }
    }

    public void Die()
    {
        this.playerCtrl.Character.RagdollCtrl.EnableRagdoll();
    }

    public void TakeDamage(int damage, Vector3 force, Vector3 hitPoint, Rigidbody hitRigidbody)
    {
        throw new System.NotImplementedException();
    }
}
