public class EnemySemiCtrl : EnemyCtrl
{
    public override void PlayDetectSound()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_ORDE_DETECT_ANGRY001);
        }
    }

    public override void PlayDeathSound()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_ORDE_DIE_HIT007);
        }
    }
}
