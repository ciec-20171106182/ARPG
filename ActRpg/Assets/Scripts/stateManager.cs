using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateManager :IActorManagerInterface
{
   // public ActorManager am;
    public float HP = 15.0f;
    public float HPMax = 15.0f;
    // Start is called before the first frame update
    public bool isGound;
    public bool isJump;
    public bool isFall;
    public bool isRun;
    public bool isRoll;
    public bool isHit;
    public bool isAttack;
    public bool isDefense;
    public bool isDie;
    public bool isBlock;
   
    public void AddHP(float value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPMax);
        if (HP > 0)
        {
            am.Hit();
        }
        else
        {
            am.Die();
        }
    }
    public void test()
    {
        print(HP);
    }
    public void Start()
    {
        HP = HPMax;
    }
    public void Update()
    {
        isGound=am.ac.checkState("ground");
        isJump= am.ac.checkState("run_jump");
        isFall=am.ac.checkState("fall");
        isRun= am.ac.checkState("run");
        isRoll= am.ac.checkState("roll");
        isHit= am.ac.checkState("beHit");
        isAttack= am.ac.checkStateTag("attack1a");
        isDefense = am.ac.checkState("defenseB", "defense");
        isDie = am.ac.checkState("die");
        isBlock = am.ac.checkState("blocked");
    }
}
