using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{
    public CapsuleCollider weaponCol;
   //// Start is called before the first frame update
   void Start()
   {
        weaponCol.enabled = false;
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
