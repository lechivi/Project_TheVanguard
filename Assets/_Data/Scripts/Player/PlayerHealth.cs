using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : PlayerAbstract, IHealth
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    private int defence;
    private int agility;
    private bool isDeath;
    [SerializeField] private int x = 5;

    private void Start()
    {
        if (this.playerCtrl.Character)
        {
            this.maxHealth = this.playerCtrl.Character.CharacterData.Health + this.playerCtrl.Character.CharacterData.HitPoint * x;
            this.currentHealth = this.maxHealth;
            this.defence = playerCtrl.Character.CharacterData.Defence;
            this.agility = playerCtrl.Character.CharacterData.Agility;
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
        
        float damageTaken = (float)damage * (10f / (10f + (float)this.defence + (float)this.agility/2));

        this.currentHealth -= Mathf.RoundToInt(damageTaken);
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
