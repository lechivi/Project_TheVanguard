using UnityEngine;

public interface IAlliance
{
    AllianceType GetAllianceType();
    Transform GetCenterTransform();
    void TakeDamge(int damage);
}
