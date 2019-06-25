using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{
    public CapsuleCollider weaponCol;
    public ActorManager am;

    public GameObject whLeft;
    public GameObject whRight;
   //// Start is called before the first frame update
   void Start()
   {
        weaponCol.enabled = false;
        weaponCol = whRight.GetComponentInChildren<CapsuleCollider>();

    }
   //
   //// Update is called once per frame
   //void Update()
   //{
   //    
   //}
   public void weaponEnable()
    {
        weaponCol.enabled = true;
    }
    public void weaponDisable()
    {
        weaponCol.enabled = false;
    }
}
