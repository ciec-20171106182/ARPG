using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftArmAinmatorFix : MonoBehaviour
{
    private Animator anim;
    public Vector3 Angles;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnAnimatorIK()
    {
        if (anim.GetBool("defense") == false) { 
            Transform LeftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
        LeftLowerArm.localEulerAngles += Angles * 0.75f;
        anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(LeftLowerArm.localEulerAngles));
    }
    }
}
