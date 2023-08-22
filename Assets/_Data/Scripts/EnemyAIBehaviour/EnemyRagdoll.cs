using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRagdoll : EnemyAbstract
{
    [SerializeField] private List<Rigidbody> listRagdollRigidbody = new List<Rigidbody>();

    protected override void LoadComponent()
    {
        base.LoadComponent();
        Rigidbody[] rigidbodies = transform.parent.Find("Root").GetComponentsInChildren<Rigidbody>();
        if (this.listRagdollRigidbody.Count != rigidbodies.Length)
        {
            this.listRagdollRigidbody.Clear(); ;
            for (int i = 0; i < rigidbodies.Length; i++)
            {
                this.listRagdollRigidbody.Add(rigidbodies[i]);

                HitBox hitBox = this.listRagdollRigidbody[i].transform.GetComponent<HitBox>();
                if (hitBox != null)
                {
                    if (!hitBox.IsSetup())
                        hitBox.Setup(this.listRagdollRigidbody[i], this.enemyCtrl.EnemyHealth.gameObject);
                    continue;
                }

                HitBox hitBoxAdd = this.listRagdollRigidbody[i].transform.AddComponent<HitBox>();
                hitBoxAdd.Setup(this.listRagdollRigidbody[i], this.enemyCtrl.EnemyHealth.gameObject);
            }
        }
    }

    public void DisableRagdoll()
    {
        for (int i = 0; i < this.listRagdollRigidbody.Count; i++)
        {
            this.listRagdollRigidbody[i].isKinematic = true;
        }
        this.enemyCtrl.Animator.enabled = true;
        this.enemyCtrl.CharacterController.enabled = true;
    }

    public void EnableRagdoll()
    {
        this.enemyCtrl.Animator.enabled = false;
        this.enemyCtrl.CharacterController.enabled = false;

        for (int i = 0; i < this.listRagdollRigidbody.Count; i++)
        {
            this.listRagdollRigidbody[i].isKinematic = false;
        }
    }

    public void TriggerRagdoll(Vector3 force, Vector3 hitPoint)
    {
        this.EnableRagdoll();
        Rigidbody hitRigidbody = this.listRagdollRigidbody.OrderBy(rb => Vector3.Distance(rb.position, hitPoint)).First();
        hitRigidbody.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);
    }

    public void TriggerRagdoll(Vector3 force, Vector3 hitPoint, Rigidbody hitRigidbody)
    {
        this.EnableRagdoll();
        hitRigidbody.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);
    }
}
