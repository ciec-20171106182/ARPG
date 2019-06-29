using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateManager :IActorManagerInterface
{
   // public ActorManager am;
    public float HP = 15.0f;
    public float HPMax = 15.0f;
    // Start is called before the first frame update
    [Header("一级基础状态")]
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

    [Header("二级混合状态")]
    public bool isImmortal;

    public void AddHP(float value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPMax);
        
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
        
        isRoll= am.ac.checkState("roll");
        isHit= am.ac.checkState("beHit");
        isAttack= am.ac.checkStateTag("attack");
        isDefense = am.ac.checkState("defenseB", "defense");
        isDie = am.ac.checkState("die");
        isBlock = am.ac.checkState("blocked");

        isImmortal = isRoll;
    }
}
