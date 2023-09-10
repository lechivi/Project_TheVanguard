using UnityEngine;

public interface IInfoScanner
{
    FactionType GetFactionType();
    Transform GetTransform();
    Transform GetCenterPoint();
    string GetTargetName();
    int GetTargetLevel();
    IHealth GetHealth();
    bool CanScan();
}
