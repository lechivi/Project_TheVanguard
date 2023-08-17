using UnityEngine;

public class EnemyDebuffs : EnemyAbstract
{
    [SerializeField] private DebuffsType currentDebuffs;

    public DebuffsType CurrentDebuffs { get => this.currentDebuffs; }

    public void ResetDebuffs()
    {
        this.currentDebuffs = DebuffsType.None;

    }

    public void Slow()
    {
        this.currentDebuffs = DebuffsType.Low;

    }

    public void Stun()
    {
        this.currentDebuffs = DebuffsType.Stun;

    }

    public void Electrocuted()
    {
        this.currentDebuffs = DebuffsType.Electrocuted;

    }
}
