using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public float walkspeed=1.4f;
    public float runspeed = 2.7f;
    public float junmVelcity = 4.0f;
    [SerializeField]
    private Animator anim;
    private Rigidbody rigi;
    private Vector3 movingVec;
    private Vector3 thrustVec;
    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigi =GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float targetRunMunlti= ((pi.run) ? 2.0f : 1.0f);
        anim.SetFloat("forword", pi.Dmag * Mathf.Lerp(anim.GetFloat("forword"),targetRunMunlti,0.3f));//减缓站立动画或走路动画切换到奔跑动画的速度

       if(pi.jump)
        {
            if (pi.run)
            {
                anim.SetTrigger("run_jump");
            }
           
            
        }
        if (pi.Dmag>0.1f)
        {
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);//球形线性插值，减缓旋转速度
            model.transform.forward = targetForward;
        }
        
        movingVec = pi.Dmag * model.transform.forward * walkspeed * ((pi.run) ? runspeed : 1.0f);
    }
    private void FixedUpdate()
    {
        //rigi.position += movingVec * Time.fixedDeltaTime;
        rigi.velocity = new Vector3(movingVec.x, rigi.velocity.y, movingVec.z)+thrustVec;
        thrustVec = Vector3.zero;
    }




    public void onenterRun_jump()
    {
       
        thrustVec = new Vector3(0, junmVelcity, 0);
    }
    public void onexitRun_jump()
    {
        
    }
   public void isGound()
    {
        anim.SetBool("isGound", true);
    }
    public void isNotGound()
    {
        anim.SetBool("isGound", false);
    }
}
