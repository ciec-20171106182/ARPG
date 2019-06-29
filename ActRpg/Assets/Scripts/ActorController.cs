using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public GameConTrollerInput pi;
    public cameraController cemer;
    public float walkspeed = 1.4f;
    public float runspeed = 2.7f;
    public float junmVelcity = 4.0f;
    public float rollVelicty = 1.0f;
    public float rollLimitSpeed = 10.0f;
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
    private float lerpTarget;
    private Vector3 deltaPos;
    //private bool tackDirection ;


    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<GameConTrollerInput>();
        anim = model.GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        


    }

    // Update is called once per frame
    void Update()
    {
        
        if (pi.LockOn)
        {
            cemer.lockUNLock();
        }
        float targetRunMunlti = ((pi.run) ? 2.0f : 1.0f);
        if (cemer.lockState == false)
        {
            anim.SetFloat("forword", pi.Dmag * Mathf.Lerp(anim.GetFloat("forword"), targetRunMunlti, 0.5f));
            anim.SetFloat("right", 0);
        }
        else
        {
            Vector3 localDvec = transform.InverseTransformDirection(pi.Dvec);
            anim.SetFloat("forword", localDvec.z * targetRunMunlti);
            anim.SetFloat("right", localDvec.x * targetRunMunlti);
        }
        //anim.SetFloat("forword", pi.Dmag * Mathf.Lerp(anim.GetFloat("forword"),targetRunMunlti,0.5f));//减缓站立动画或走路动画切换到奔跑动画的速度


        if (pi.defenseOn && canAttack)
        {
            anim.SetBool("defense", true);
        }
        if (pi.defenseOff && canAttack)
        {
            anim.SetBool("defense", false);
        }

        if (pi.jump)
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

        if ((pi.attack||pi.shiedHit) && (checkState("ground")||checkStateTag("attack")) && canAttack)
        {
           if(pi.shiedHit)
            {
                anim.SetBool("Right0Lift1",true);

                anim.SetTrigger("attack");
                
            }
             if(pi.attack)
            {
                anim.SetBool("Right0Lift1", false);
                
                anim.SetTrigger("attack");
            }   
        }
        if (cemer.lockState == false)
        {
            if (pi.Dmag > 0.1f)
            {
                Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);//球形线性插值，减缓旋转速度
                model.transform.forward = targetForward;
            }
            movingVec = pi.Dmag * model.transform.forward * walkspeed * ((pi.run) ? runspeed : 1.0f);
            
        }
        else
        {
            transform.forward = cemer.lockDirection;
            model.transform.forward = transform.forward;
            movingVec = pi.Dvec * walkspeed * ((pi.run) ? runspeed : 1.0f);
        }

        if (rigi.velocity.magnitude > rollLimitSpeed)
        {
            anim.SetTrigger("limitRoll");
        }
        //print("当前动画状态为：" + anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);

    }
    private void FixedUpdate()
    {
        //rigi.position += movingVec * Time.fixedDeltaTime;
        rigi.position += deltaPos;
        rigi.velocity = new Vector3(movingVec.x, rigi.velocity.y, movingVec.z) + upThrustVec + forwardThrustVec;
        upThrustVec = Vector3.zero;
        forwardThrustVec = Vector3.zero;
        deltaPos = Vector3.zero;

    }

    public bool checkState(string stateName, string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return result;
    }
    public bool checkStateTag(string tagName, string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(tagName);
        return result;
    }


    public void onenterRun_jump()
    {

        upThrustVec = new Vector3(0, junmVelcity, 0);//给一个向上的冲量，使人物跳起。
        //tackDirection = true;
    }
    public void onexitRun_jump()
    {

    }
    public void onRoll()
    {
        canAttack = false;
        forwardThrustVec = new Vector3(0, rollVelicty, 0);//给一个up的冲量，使人物翻滚更流畅。
        rigi.velocity = new Vector3(5.0f, rigi.velocity.y, 5.0f);
       // tackDirection = true;
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
    public void onattackidleEnter()
    {
        pi.inputEnabled = true;
        // anim.SetLayerWeight(anim.GetLayerIndex("attack"), 0);
        //lerpTarget = 0f;
    }
   
    public void onattack1aEnter()
    {
        pi.inputEnabled = false;
       // lerpTarget = 1.0f;
    }
    public void onBeHitEnter()
    {
        pi.inputEnabled = false;
    }
    public void onattackUpdata()
    {
        forwardThrustVec = model.transform.forward * anim.GetFloat("attackVelocity");
       //  float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
       // currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
       // anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }
    public void onUpdateRootMove(object deltaPos2)
    {
        if (checkState("attack1c"))
        {
            deltaPos += (Vector3)deltaPos2;
        }
    }
   public void isSueSetTrigger(string tiggerName)
    {
        anim.SetTrigger(tiggerName);
    }
    public void OnDieEnter()
    {
        pi.inputEnabled = false;
    }
    public void OnBlockedEnter()
    {
        pi.inputEnabled = false;
    }
}
