using UnityEngine;

public class TestUpdate : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (this.enemyHealth == null) return;
            this.enemyHealth.TakeDamage(2);
        }
    }
}
