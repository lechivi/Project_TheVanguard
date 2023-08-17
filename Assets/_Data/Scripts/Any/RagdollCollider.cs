using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RagdollCollider : MonoBehaviour
{
    [SerializeField] private List<Collider> colliders;

    [SerializeField] private BoxCollider hand_L;
    [SerializeField] private BoxCollider hand_R;
    [SerializeField] private BoxCollider ankle_L;
    [SerializeField] private BoxCollider ankle_R;
    [SerializeField] private bool isLeftYet;

    private void LoadColliders()
    {
        Debug.Log("LoadColliders");
        Collider[] partRagdoll = transform.GetComponentsInChildren<Collider>();
        this.colliders.Clear();
        foreach (Collider c in partRagdoll)
        {
            this.colliders.Add(c);
            if (c.gameObject.name == "Hand_L")
            {
                this.hand_L = c as BoxCollider;
            }
            else if (c.gameObject.name == "Hand_R")
            {
                this.hand_R = c as BoxCollider;
            }
            else if (c.gameObject.name == "Ankle_L")
            {
                this.ankle_L = c as BoxCollider;
            }
            else if (c.gameObject.name == "Ankle_R")
            {
                this.ankle_R = c as BoxCollider;
            }
        }
    }

    private void DulicateLeftSide()
    {
        if (this.colliders == null) return;

        BoxCollider handSideDone = this.isLeftYet ? this.hand_L : this.hand_R;
        BoxCollider handSideTarget = this.isLeftYet ? this.hand_R : this.hand_L;
        BoxCollider ankleSideDone = this.isLeftYet ? this.ankle_L : this.ankle_R;
        BoxCollider ankleSideTarget = this.isLeftYet ? this.ankle_R : this.ankle_L;
        handSideTarget.center = new Vector3(-handSideDone.center.x, -handSideDone.center.y, -handSideDone.center.z);
        handSideTarget.size = handSideDone.size;
        ankleSideTarget.center = new Vector3(-ankleSideDone.center.x, -ankleSideDone.center.y, -ankleSideDone.center.z);
        ankleSideTarget.size = ankleSideDone.size;
    }

    public void ButtonLoadColliders()
    {
        this.LoadColliders();
    }

    public void ButtonDulicateLeftSide()
    {
        this.DulicateLeftSide();
    }
}

[CustomEditor(typeof(RagdollCollider), true)]
public class RagdollChangeSizeCustomInscpector : Editor
{
    public override void OnInspectorGUI()
    {
        RagdollCollider ragdollChangeSize = (RagdollCollider)target;
        if (GUILayout.Button("Load Colliders"))
        {
            ragdollChangeSize.ButtonLoadColliders();
        }

        if (GUILayout.Button("Dulicate Left Side"))
        {
            ragdollChangeSize.ButtonDulicateLeftSide();
        }

        DrawDefaultInspector();
    }
}
