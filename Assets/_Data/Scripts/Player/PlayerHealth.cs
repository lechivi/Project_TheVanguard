using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : PlayerAbstract
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private bool isDeath;

    public int MaxHealth { get => this.maxHealth; }
    public int CurrentHealth { get => this.currentHealth; }
    public bool IsDeath { get => this.isDeath; }

    private void Start()
    {
        if (this.playerCtrl.Character)
        {
            this.maxHealth = this.playerCtrl.Character.CharacterData.Health;
            this.currentHealth = this.maxHealth;
        }
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
        Debug.Log("Player HP = 0");
    }
}
