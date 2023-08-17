using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "SO/CharacterData")]
public class CharacterDataSO : ScriptableObject
{
    [Header("OVERVIEW")]
    public string CharacterName;
    public string ClassName;
    public Species Species;
    public Sprite CharacterIcon;
    public GameObject CharacterModel;
    [TextAreaAttribute(5, 10)] public string CharacterDescription;

    [Header("STATS")]
    [Range(50, 100)] public int Health = 50;
    [Range(0, 10)] public int Power = 5;
    [Range(0, 10)] public int Defence = 5;
    [Range(0, 10)] public int HitPoint = 5;
    public float CooldownSkillTime;

    [Header("SKILL")]
    public string SpecialSkillName;
    public Sprite SpecialSkillIcon;
    [TextAreaAttribute(3, 8)] public string SpecialSkillDescription;

    [Header("SKIN")]
    /// <summary>
    ///(string) The first skin is set as default skin
    /// </summary>
    public List<GameObject> ListSkin;
}
