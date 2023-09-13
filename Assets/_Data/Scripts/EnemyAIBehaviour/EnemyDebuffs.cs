using UnityEngine;

public class EnemyDebuffs : EnemyAbstract
{
    [SerializeField] private DebuffsType currentDebuffs;
    //public float TimeLow;
    //public float TimeStun;
    //public float TimeElectrocuted;

    public DebuffsType CurDebuff { get => this.currentDebuffs; }

    public void ResetDebuffs()
    {
        this.currentDebuffs = DebuffsType.None;

        if (!this.enemyCtrl.EnemyHealth.IsDeath())
            this.enemyCtrl.Animator.Rebind();
        //this.TimeLow = 0f;
        //this.TimeStun = 0f;
        //this.TimeElectrocuted = 0f;
    }

    public void Slow(float timeDebuff)
    {
        this.currentDebuffs = DebuffsType.Low;
        //this.TimeLow = timeDebuff;
    }

    public void Stun(float timeDebuff)
    {
        this.currentDebuffs = DebuffsType.Stun;
        //this.TimeStun = timeDebuff;
    }
    
    public void Electrocuted(float timeDebuff)
    {
        this.currentDebuffs = DebuffsType.Electrocuted;
        //this.TimeElectrocuted = timeDebuff;

        this.enemyCtrl.Animator.SetTrigger("Electrocuted");

        CancelInvoke("ResetDebuffs");
        Invoke("ResetDebuffs", timeDebuff);
    }

    private void Update()
    {
        
    }
}
