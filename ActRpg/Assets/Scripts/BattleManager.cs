using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManagerInterface
{
    //public ActorManager am;
    
    public CapsuleCollider defCol;
    // // Start is called before the first frame update
    void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        defCol.center =Vector3.up*1.0f;
        defCol.height = 1.0f;
        defCol.radius = 1.0f;
        defCol.isTrigger = true;

    }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag==("Weapon"))
        {
            
            am.tryDoManager();
            print(col.name);
            
        }
    }
}