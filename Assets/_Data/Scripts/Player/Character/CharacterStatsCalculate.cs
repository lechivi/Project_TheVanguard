using UnityEngine;

public class CharacterStatsCalculate
{
    public static int MaxHealth(CharacterDataSO chrData)
    {
        int baseHealth = 50;
        int maxHealth = baseHealth + chrData.HitPoint * 5;
        return maxHealth;
    }

    public static int DamgeDeal(CharacterDataSO chrData, WeaponDataSO weaponData)
    {
        int powerChr = chrData.Power;
        int meleeDamage = (int)weaponData.MeleeDamage;
        int damgeDeal = (int)(meleeDamage * (1 + (float)(powerChr / (powerChr + 12))));
        return damgeDeal;
    }

    public static int DamageTake(CharacterDataSO chrData, int damage)
    {
        int defence = chrData.Defence;
        int agility = chrData.Agility;
        float damageTaken = (float)damage * (10f / (10f + defence + agility / 2f));
        int damgeTake = Mathf.RoundToInt(damageTaken);
        return damgeTake;
    }

    public static float Speed(CharacterDataSO chrData)
    {
        float baseSpeed = 1f;
        float speed = baseSpeed * (10f / (10f - chrData.Agility));
        return speed;
    }
}
