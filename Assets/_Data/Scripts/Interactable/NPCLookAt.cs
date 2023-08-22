using UnityEngine;

public class NPCLookAt : MonoBehaviour
{
    public void LookAtTarget(Transform target)
    {
        transform.LookAt(target);
    }
}
