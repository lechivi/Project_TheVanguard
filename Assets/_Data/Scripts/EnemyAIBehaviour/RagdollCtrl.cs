using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RagdollCtrl : SaiMonoBehaviour
{
    [SerializeField] private List<Rigidbody> listRagdollRigidbody = new List<Rigidbody>();
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;

    public Animator Animator { get => this.animator; set => this.animator = value; }
    public CharacterController CharacterController { get => this.characterController; set => this.characterController = value; }

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

                //HitBox hitBox = this.listRagdollRigidbody[i].transform.GetComponent<HitBox>();
                //if (hitBox != null)
                //{
                //    if (!hitBox.IsSetup())
                //        hitBox.Setup(this.listRagdollRigidbody[i], this.enemyCtrl.EnemyHealth.gameObject);
                //    continue;
                //}
                //HitBox hitBoxAdd = this.listRagdollRigidbody[i].transform.AddComponent<HitBox>();
                //hitBoxAdd.Setup(this.listRagdollRigidbody[i], this.enemyCtrl.EnemyHealth.gameObject);
                if (rigidbodies[i].GetComponent<HitBox>() == null) 
                {
                    rigidbodies[i].AddComponent<HitBox>();
                }
            }
        }
    }

    public void DisableRagdoll()
    {
        this.SetKinematic(1);
        this.animator.enabled = true;
        this.characterController.enabled = true;
    }

    public void EnableRagdoll()
    {
        this.animator.enabled = false;
        this.characterController.enabled = false;
        this.SetKinematic();

        Invoke("SetKinemactic", 2f);
    }

    private void SetKinematic(int isKinematic = 0)
    {
        Debug.Log(isKinematic);
        for (int i = 0; i < this.listRagdollRigidbody.Count; i++)
        {
            this.listRagdollRigidbody[i].isKinematic = isKinematic == 1 ? true : false;
        }
    }

    public Rigidbody ClosestRigidbody(Vector3 hitPoint)
    {
        Rigidbody hitRigidbody = this.listRagdollRigidbody.OrderBy(rb => Vector3.Distance(rb.position, hitPoint)).First();
        return hitRigidbody;
    }
}
