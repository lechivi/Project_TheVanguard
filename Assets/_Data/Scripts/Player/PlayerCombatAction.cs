using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatAction : PlayerAbstract
{
    private CombatAction combatActionMouseL;
    private CombatAction combatActionMouseR;

    public void ActionMouseL()
    {
        switch (this.combatActionMouseL)
        {
            case CombatAction.RangedWeapon:

                break;

            case CombatAction.MeleeWeapon:

                break;

            case CombatAction.CharacterSpecific:
                Character character = this.playerCtrl.Character;
                if (character)
                {
                    character.ActionMouseL();
                }
                break;

            default:
                //Debug.LogError("Not set combat action for Mouse-Left");
                break;
        }
    }

    public void ActionMouseR()
    {
        switch (this.combatActionMouseR)
        {
            case CombatAction.RangedWeapon:

                break;

            case CombatAction.MeleeWeapon:

                break;

            case CombatAction.CharacterSpecific:
                Character character = this.playerCtrl.Character;
                if (character)
                {
                    character.ActionMouseL();
                }
                break;

            default:
                //Debug.LogError("Not set combat action for Mouse-Right");
                break;
        }
    }

    public void SpecialSkill()
    {
        Character character = this.playerCtrl.Character;
        if (character)
        {
            character.SpecialSkill();
        }
    }

    public void BattleSkill()
    {
        Character character = this.playerCtrl.Character;
        if (character)
        {
            character.BattleSkill();
        }
    }

    public void SetActionMouseLeft(bool isCharacter)
    {
        if (isCharacter)
        {
            this.combatActionMouseL = CombatAction.CharacterSpecific;
        }
        else
        {
            Debug.Log("Set it back to weapon");
            //if (this.playerCtrl.PlayerWeapon.PlayerWeaponActive == null) return;
            //if (this.AttackInput)
            //{
            //    this.playerCtrl.PlayerWeapon.PlayerWeaponActive.isFiring = true;
            //}
            //else if (!this.AttackInput)
            //{
            //    this.playerCtrl.PlayerWeapon.PlayerWeaponActive.isFiring = false;
            //}
        }
    }

    public void SetActionMouseRight(bool isCharacter)
    {
        if (isCharacter)
        {
            this.combatActionMouseR = CombatAction.CharacterSpecific;
        }
        else
        {
            Debug.Log("Set it back to weapon");
        }
    }
}
