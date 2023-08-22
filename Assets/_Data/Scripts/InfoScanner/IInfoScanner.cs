using UnityEngine;

public interface IInfoScanner
{
    FactionType GetFactionType();
    Transform GetCenterPoint();
    string GetTargetName();
    int GetTargetLevel();
    IHealth GetHealth();
    bool CanScan();
}
