using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject healthGameObject;

    public GameObject HealthGameObject { get => this.healthGameObject; set => this.healthGameObject = value; }

    public void Setup(Rigidbody rb, GameObject healthGameObject)
    {
        this.rb = rb;
        this.healthGameObject = healthGameObject;
    }

    public void OnHit(int damage, Vector3 force, Vector3 hitPoint)
    {
        if (this.healthGameObject == null || this.healthGameObject == null) return;

        this.healthGameObject.GetComponent<IHealth>().TakeDamage(damage, force, hitPoint, this.rb);
    }

    public bool IsSetup()
    {
        return this.rb != null && this.healthGameObject != null;
    }
}
