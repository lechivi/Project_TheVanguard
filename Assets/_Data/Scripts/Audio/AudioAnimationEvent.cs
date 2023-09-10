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
            AudioManager.Instance.PlaySe(AUDIO.SE_WEAMEL_AXE2);
        }
    }

    //Unarmed
    public void UnarmedAttack()
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

}
