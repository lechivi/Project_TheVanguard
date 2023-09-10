using UnityEngine;

public class NPCLookAt : MonoBehaviour
{
    public void LookAtTarget(Transform target)
    {
        transform.LookAt(new Vector3( target.position.x, transform.position.y, target.position.z));
    }
}
