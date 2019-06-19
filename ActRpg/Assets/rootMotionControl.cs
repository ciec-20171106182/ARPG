using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootMotionControl : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void OnAnimatorMove()
    {
        SendMessageUpwards("onUpdateRootMove", (object)anim.deltaPosition);
    }
}
