using UnityEngine;

public interface IHealth
{
    int GetMaxHealth();
    int GetCurrentHealth();
    bool IsDeath();
    void TakeDamage(int damage);
    void TakeDamage(int damage, Vector3 force, Vector3 hitPoint, Rigidbody hitRigidbody = null);
}
