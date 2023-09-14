using System.Collections;
using UnityEngine;

public class HandleEndDungeon : MonoBehaviour
{
    [SerializeField] private MovingPlatform movingPlatform;
    [SerializeField] private EnemyCtrl boss;
    [SerializeField] private GameObject portalObject;

    private bool check = true;

    private void FixedUpdate()
    {
        if (this.boss.EnemyHealth.IsDeath() && this.check)
        {
            this.check = false;
            StartCoroutine(this.ArrayAction());
        }

        if (Input.GetKeyDown(KeyCode.T)) 
        {
            StartCoroutine(this.ArrayAction());

        }
    }

    private IEnumerator ArrayAction()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WIN_NOTIFICATIONS_9);
            AudioManager.Instance.PlayBgm(AUDIO.BGM_WIN_STEALTHY_NIGHT_SHADOW, 1.5f);
        }

        yield return new WaitForSeconds(1f);

        //play sound
        this.movingPlatform.TriggerMove();
        yield return new WaitForSeconds(1.5f);

        this.portalObject.SetActive(true);
    }

}
