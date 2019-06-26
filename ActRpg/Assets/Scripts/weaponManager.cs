using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weaponManager :IActorManagerInterface
{
    public CapsuleCollider weaponLCol;
    public CapsuleCollider weaponRCol;
  //  public ActorManager am;
    

    public GameObject whLeft;
    public GameObject whRight;
   //// Start is called before the first frame update
   void Start()
   {
        
        weaponRCol = whRight.GetComponentInChildren<CapsuleCollider>();
        weaponLCol = whLeft.GetComponentInChildren<CapsuleCollider>();
        weaponRCol.enabled = false;
        weaponLCol.enabled = false;
    }
   //
   //// Update is called once per frame
   //void Update()
   //{
   //    
   //}
   public void weaponEnable()
    {
        weaponRCol.enabled = true;

        weaponLCol.enabled = true;
    }
    public void weaponDisable()
    {
        weaponRCol.enabled = false;
        weaponLCol.enabled = false;
    }
}
