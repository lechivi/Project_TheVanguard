using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnimationEventRig : MonoBehaviour
{
    //Shot
    public void AssaultRifleShotSE()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WEARAN_RIFLE_SHOT_ASSAULT_A_SHTL_SINGLE_01);
        }
    }
    public void ShotgunShotSE()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WEARAN_SHOTGUN_SHOT_SHOTGUN_A_SHTL_SINGLE_01);
        }
    }
    public void SniperShotSE()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WEARAN_SNIPER_SHOT_RIFLE_A_SHTL_SINGLE_01);
        }
    }
    public void PistolShotSE()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WEARAN_PISTOL_SHOT_HANDGUN_B_SHOT_SINGLE_03);
        }
    }

    //Equip
    public void GunEquipSE()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WEARAN_DROP_WEAPON_FOLEY_DROP_01);
        }
    }


    public void MeleeEquipSE()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WEA_PICKUP_METALITEMPICKUP);
        }
    }

    // Holster

    public void HolsterSE()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WEARAN_PISTOL_EQMPTY_HANDGUN_B_EMPTY);
        }
    }

    // Reload
    public void ReloadSE()  //ASSAULT RIFLE , SNIPER, SHOTGUN
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WEARAN_RIFLE_RELOAD_ASSAULT_C_RELOAD_01);
        }
    }

    public void ReloadPistolSE() //PISTOL
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WEARAN_RIFLE_RELOAD_ASSAULT_C_RELOAD_01);
        }
    }

    public void ShotgunSlowHandReloadSE()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WEARAN_SHOTGUN_RELOAD_SHOTGUN_A_RELOAD_01);
        }
    }

}
