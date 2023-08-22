using UnityEngine;

public class PlayerInfoScanner : SaiMonoBehaviour, IInfoScanner
{
    [SerializeField] private Character character;

    public PlayerInfoScanner(Character character)
    {
        this.character = character;
    }

    public FactionType GetFactionType()
    {
        return FactionType.Alliance;
    }

    public Transform GetCenterPoint()
    {
        throw new System.NotImplementedException();
    }

    public string GetTargetName()
    {
        return gameObject.name;
    }

    public int GetMaxHealth()
    {
        throw new System.NotImplementedException();
    }

    public int GetCurrentHealth()
    {
        throw new System.NotImplementedException();
    }

    public int GetTargetLevel()
    {
        return 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
