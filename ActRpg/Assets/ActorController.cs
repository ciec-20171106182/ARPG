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
    public float rollVelicty = 1.0f;
    public float rollLimitSpeed =10.0f;
    public PhysicMaterial PhysicMaterialOne;
    public PhysicMaterial PhysicMaterialZero;
    [SerializeField]
    private Animator anim;
    private Rigidbody rigi;
    private Vector3 movingVec;
    private Vector3 upThrustVec;
    private Vector3 forwardThrustVec;
    private bool canAttack;
    private CapsuleCollider col;
    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigi =GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
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
        if (pi.jump)
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

        if(pi.attack&&checkState("ground")&&canAttack)
        {
            anim.SetTrigger("attack");
        }
        if (pi.Dmag>0.1f)
        {
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);//球形线性插值，减缓旋转速度
            model.transform.forward = targetForward;
        }
        if(rigi.velocity.magnitude>rollLimitSpeed)
        {
            anim.SetTrigger("limitRoll");
        }
        
        movingVec = pi.Dmag * model.transform.forward * walkspeed * ((pi.run) ? runspeed : 1.0f);
    }
    private void FixedUpdate()
    {
        //rigi.position += movingVec * Time.fixedDeltaTime;
        rigi.velocity = new Vector3(movingVec.x, rigi.velocity.y, movingVec.z)+upThrustVec+forwardThrustVec;
        upThrustVec = Vector3.zero;
        forwardThrustVec = Vector3.zero;
    }

    private bool checkState(string stateName,string layerName="Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return result; 
    }


    public void onenterRun_jump()
    {
       
        upThrustVec = new Vector3(0, junmVelcity, 0);//给一个向上的冲量，使人物跳起。
    }
    public void onexitRun_jump()
    {
        
    }
    public void onRoll()
    {
        canAttack = false;
        forwardThrustVec = new Vector3(0, rollVelicty, 0);//给一个向前的冲量，使人物翻滚更流畅。
    }
    public void onGroundEnter()
    {
        canAttack = true;
        col.material = PhysicMaterialOne;
    }
    public void onGroundExit()
    {
        col.material = PhysicMaterialZero;
    }
    public void isGound()
    {
        anim.SetBool("isGound", true);
        
    }
    public void isNotGound()
    {
        anim.SetBool("isGound", false);
    }
    public void onattackidle()
    {
        pi.inputEnabled = true;
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), 0);

    }
    public void onattack1aEnter()
    {
        pi.inputEnabled = false;
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), 1.0f);
    }
    public void onattackUpdata()
    {
        forwardThrustVec = model.transform.forward * anim.GetFloat("attackVelocity");
    }
}
