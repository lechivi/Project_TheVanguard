using UnityEngine;

public class DroneHealth : SaiMonoBehaviour, IHealth
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int currentHealth;

    private bool isDeath;

    private void OnEnable()
    {
        this.currentHealth = this.maxHealth;
    }

    public int GetCurrentHealth()
    {
        return this.currentHealth;
    }

    public int GetMaxHealth()
    {
        return this.maxHealth;
    }

    public bool IsDeath()
    {
        return this.isDeath;
    }

    public void TakeDamage(int damage)
    {
        this.currentHealth -= damage;
        if (this.currentHealth <= 0 ) 
        {
            this.currentHealth = 0;
            this.Die();
        }
    }

    private void Die()
    {
        this.isDeath = true;
        Debug.Log("DronHP = 0", gameObject);
    }

    public void TakeDamage(int damage, Vector3 force, Vector3 hitPoint, Rigidbody hitRigidbody)
    {
        throw new System.NotImplementedException();
    }
}
