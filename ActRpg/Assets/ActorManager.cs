using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public BattleManager bm;
    public ActorController ac;
    // Start is called before the first frame update
    void Awake()
    {
        ac = GetComponent<ActorController>();

        GameObject sensor = transform.Find("Sensor").gameObject;
        bm = GetComponentInChildren<BattleManager>();
        if (bm==null)
        {
            bm=sensor.AddComponent<BattleManager>();
        }
        bm.am = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void doManager()
    {
        ac.isSueSetTrigger("beHit");
    }
}
