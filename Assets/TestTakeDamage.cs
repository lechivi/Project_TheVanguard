using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestTakeDamage : MonoBehaviour
{
    public float health = 100;
    public float defense = 5;

    public void TakeDamage(float damage)
    {
        float damageTaken = damage * (100 / (100 + defense));
        health -= damageTaken;
    }

    private void Update()
    {
        Debug.Log(health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
