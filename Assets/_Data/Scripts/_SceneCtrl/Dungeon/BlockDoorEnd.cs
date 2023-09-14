using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDoorEnd : SaiMonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool isClose;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.animator == null )
            this.animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        this.animator.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCollider") && !this.isClose)
        {
            this.animator.enabled = true;
            this.isClose = true;
            this.animator.SetTrigger("Close");

            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PauseBgm();
                AudioManager.Instance.PlaySe(AUDIO.SE_DOORSTONE_DOORSTONESMALL);
            }

            Invoke("PlayBossBgm", 5f);
        }
    }

    private void PlayBossBgm()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayBgm(AUDIO.BGM_BOSS_I_WILL_BEAT_YOU_EXTENDED, 5f);
        }
    }

    public void OnAnimationDoor()
    {
        this.animator.enabled = false;
    }

    //public void OpenDoor()
    //{
    //    this.animator.SetTrigger("Open");
    //}
}
