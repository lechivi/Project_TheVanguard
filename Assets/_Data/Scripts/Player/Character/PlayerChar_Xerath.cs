using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChar_Xerath : PlayerCharacter
{
    [Header("REFERENCE")]
    [SerializeField] private GameObject prefabXB;
    [SerializeField] private Animator rigAnimator;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Mesh meshToChangeTo;

    [Header("Xerath_Beta")]
    [SerializeField] private GameObject rootXB;
    [SerializeField] private GameObject meshXB;
    [SerializeField] private Avatar avatarXB;

    [Header("Xerath_Alpha")]
    [SerializeField] private Transform rootXA;
    [SerializeField] private Transform meshXA;
    [SerializeField] private Avatar avatarXA;

    [Space]
    [SerializeField] private Transform player;
    [SerializeField] private Transform container;

    private bool isBeta;

    private void OnEnable()
    {
        //this.SetForm(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //this.SetForm(false);
            this.SharedMesh();
        }
    }

    //private void SetForm(bool isBeta)
    //{
    //    this.isBeta = isBeta;

    //    Animator animator = this.playerCtrl.Animator;
    //    animator.enabled = false;
    //    animator.avatar = isBeta ? avatarXB : avatarXA;

    //    //this.rootXB.gameObject.SetActive(isBeta);
    //    //this.meshXB.gameObject.SetActive(isBeta);
    //    //this.rootXA.gameObject.SetActive(!isBeta);
    //    //this.meshXA.gameObject.SetActive(!isBeta);

    //    this.rootXB.SetParent(isBeta ? this.player : this.container);
    //    this.meshXB.SetParent(isBeta ? this.player : this.container);
    //    this.rootXA.SetParent(isBeta ? this.container : this.player);
    //    this.meshXA.SetParent(isBeta ? this.container : this.player);

    //    animator.enabled = true;
    //}

    private void CreateForm()
    {
        RuntimeAnimatorController originalController = playerCtrl.Animator.runtimeAnimatorController;
        this.playerCtrl.Animator.enabled = false;
        this.rigAnimator.enabled = false;

        Destroy(this.rootXB);
        Destroy(this.meshXB);

        GameObject root = Instantiate(this.prefabXB.transform.Find("Root").gameObject, this.player);
        root.name = "Root";
        GameObject mesh = Instantiate(this.prefabXB.transform.Find("Mesh").gameObject, this.player);
        mesh.name = "Mesh";
        mesh.GetComponent<SkinnedMeshRenderer>().rootBone = root.transform;

        this.rootXB = root;
        this.meshXB = mesh;

        this.playerCtrl.Animator.enabled = true;
        this.rigAnimator.enabled = true;

        //this.playerCtrl.Animator.Rebind();
        //this.rigAnimator.Rebind();
    }

    private void SharedMesh()
    {
        this.skinnedMeshRenderer.sharedMesh = this.meshToChangeTo;
    }
    public bool GetIsBeta()
    {
        return this.isBeta;
    }
}
