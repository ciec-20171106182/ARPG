using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public BattleManager bm;
    public ActorController ac;
    public weaponManager wm;
    public stateManager sm;
    // Start is called before the first frame update
    void Awake()
    {
        ac = GetComponent<ActorController>();

        GameObject modle = ac.model;
        GameObject sensor = transform.Find("Sensor").gameObject;
        /* bm = GetComponentInChildren<BattleManager>();
         if (bm==null)
         {
             bm=sensor.AddComponent<BattleManager>();
         }
         bm.am = this;
         wm = modle.GetComponent<weaponManager>();
         if(wm==null)
         {
             wm = modle.AddComponent<weaponManager>();
         }
         wm.am = this;
         sm = GetComponent<stateManager>();
         if (sm==null)
         {
             sm = gameObject.AddComponent<stateManager>();
         }
         sm.am = this;*/
        bm = Bind<BattleManager>(sensor);
        wm = Bind<weaponManager>(modle);
        sm = Bind<stateManager>(gameObject);
    }

    private T Bind<T>(GameObject go) where T:IActorManagerInterface
    {
        T Temp;
        Temp = go.GetComponent<T>();
        if(Temp==null)
        {
            Temp = go.AddComponent<T>();
        }
        Temp.am = this;
        return Temp;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void tryDoManager()
    {
        if (sm.HP>0)
        {
            sm.AddHP(-5);
        }

        sm.test();
    }
    public void Hit()
    {
        ac.isSueSetTrigger("beHit");
    }
    public void Die()
    {
        ac.isSueSetTrigger("die");
        ac.pi.inputEnabled = false;
        if(ac.cemer.lockState==true)
        {
            ac.cemer.lockUNLock();
        }
        ac.cemer.enabled = false;
    }
}
