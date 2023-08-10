using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AutoRefRig : SaiMonoBehaviour
{
    [Header("Add 'Clavicle_R' to source objects of 'WeaponPose'")]
    [SerializeField] private Transform weaponPose;
    [SerializeField] private Transform clavicle_R;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        Transform root = transform.parent.Find("Root");

        Transform rigLayer_BodyAim = transform.Find("RigLayer_BodyAim");
        Transform rigLayer_WeaponAim = transform.Find("RigLayer_WeaponAim");
        Transform rigLayer_HandLK = transform.Find("RigLayer_HandLK");

        rigLayer_BodyAim.Find("AimSpine1").GetComponent<MultiAimConstraint>().data.constrainedObject
            = root.Find("Hips").Find("Spine_01");
        rigLayer_BodyAim.Find("AimSpine2").GetComponent<MultiAimConstraint>().data.constrainedObject
            = root.Find("Hips").Find("Spine_01").Find("Spine_02");
        rigLayer_BodyAim.Find("BodyRecoil").GetComponent<OverrideTransform>().data.constrainedObject
            = root.Find("Hips").Find("Spine_01").Find("Spine_02");
        rigLayer_BodyAim.Find("AimHead").GetComponent<MultiAimConstraint>().data.constrainedObject
            = root.Find("Hips").Find("Spine_01").Find("Spine_02").Find("Spine_03").Find("Neck").Find("Head");

        this.weaponPose = rigLayer_WeaponAim.Find("WeaponPose");
        this.clavicle_R = root.Find("Hips").Find("Spine_01").Find("Spine_02").Find("Spine_03").Find("Clavicle_R");

        TwoBoneIKConstraint leftHand = rigLayer_HandLK.Find("LeftHand").GetComponent<TwoBoneIKConstraint>();
        leftHand.data.root = root.Find("Hips").Find("Spine_01").Find("Spine_02").Find("Spine_03").Find("Clavicle_L").Find("Shoulder_L");
        leftHand.data.mid = leftHand.data.root.Find("Elbow_L");
        leftHand.data.tip = leftHand.data.mid.Find("Hand_L");
        TwoBoneIKConstraint rightHand = rigLayer_HandLK.Find("RightHand").GetComponent<TwoBoneIKConstraint>();
        rightHand.data.root = root.Find("Hips").Find("Spine_01").Find("Spine_02").Find("Spine_03").Find("Clavicle_R").Find("Shoulder_R");
        rightHand.data.mid = leftHand.data.root.Find("Elbow_R");
        rightHand.data.tip = leftHand.data.mid.Find("Hand_R");

        Debug.Log("Add 'Clavicle_R' to source objects of 'WeaponPose'", clavicle_R.gameObject);
    }
}
