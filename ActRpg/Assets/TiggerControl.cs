using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerControl : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ResetTigger(string triggerName)
    {
        anim.ResetTrigger(triggerName);
    }
}
