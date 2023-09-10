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
        int Agility = Mathf.RoundToInt(this.agility / 2);
        int damageTaken = damage * (10 / (10 + defence + Agility));
        this.currentHealth -= damageTaken;
        if (this.currentHealth <= 0)
        {
            this.currentHealth = 0;
            this.isDeath = true;
            this.Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player HP = 0");
    }

    public void TakeDamage(int damage, Vector3 force, Vector3 hitPoint, Rigidbody hitRigidbody)
    {
        throw new System.NotImplementedException();
    }
}
