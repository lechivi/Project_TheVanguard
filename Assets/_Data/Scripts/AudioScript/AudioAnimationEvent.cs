using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnimationEvent : MonoBehaviour
{
    //Axe
    public void AxeAttack()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_PUNCH_WHOOSH_05);
        }
    }

    //Unarmed
    public void SE_UnarmedAttack()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WEAUNA_SYNTH_WHOOSH_2);
        }
    }

    //Knife
    public void KnifeAttack()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WEAMEL__EQUIPWEAPON2);
        }
    }

    public void SE_TranSorm()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_TRANSFORM);
        }
    }

    public void SE_IronStoneImpact()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_CHR_IRONSTONE_IMPACT);
        }
    }

    public void SE_SeraEffect()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_CHR_SERA_SKILL_LIGHTNING_SPELL_08);
        }
    }

    public void SE_XerathTransform()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_CHR_XERATH_SKILL_RETRO_STINGER_10);
        }
    }

    public void SE_DarleneEffect()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_CHR_DARLENE_SKILL_CAST_04);
        }
    }
}
