using UnityEngine;

public class BlockIce : SaiMonoBehaviour
{
    [SerializeField] private EnemyCtrl enemyIce;

    private void FixedUpdate()
    {
        if (this.enemyIce == null) return;

        if (this.enemyIce.EnemyHealth.IsDeath())
        {
            //play sound
            gameObject.SetActive(false);
        }
    }
}
