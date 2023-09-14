using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (this.spawnPoint == null) return;

        Character character = other.GetComponentInParent<Character>();
        if (character != null)
        {
            character.gameObject.SetActive(false);
            character.CharacterTransform.position = this.spawnPoint.position;
            character.CharacterTransform.rotation = this.spawnPoint.rotation;
            character.gameObject.SetActive(true);
            return;
        }

        EnemyCtrl enemy = other.GetComponentInParent<EnemyCtrl>();
        if (enemy != null)
        {
            enemy.gameObject.SetActive(true);
            return;
        }

        other.gameObject.SetActive(false);
    }
}
