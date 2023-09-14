using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    private bool isPlayerInside = false;
    private int damageAmount = 5;
    private float damageInterval = 1f;
    private float lastDamageTime = 0f;
    private string playerTag = "PlayerCollider";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(this.playerTag))
        {
            this.isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(this.playerTag))
        {
            this.isPlayerInside = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.isPlayerInside && Time.time - this.lastDamageTime > this.damageInterval)
        {
            this.DealDamageToPlayer();
            this.lastDamageTime = Time.time;
        }
    }

    private void DealDamageToPlayer()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.PlayerCtrl.PlayerHealth.TakeDamage(this.damageAmount);
        }
    }
}
