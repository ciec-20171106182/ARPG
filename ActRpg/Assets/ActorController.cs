using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public float walkspeed=1.4f;
    [SerializeField]
    private Animator anim;
    private Rigidbody rigi;
    private Vector3 movingVec;
    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigi =GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("forword", pi.Dmag);
        if (pi.Dmag>0.1f)
        {
            model.transform.forward = pi.Dvec;
        }
        
        movingVec = pi.Dmag * model.transform.forward *walkspeed;
    }
    private void FixedUpdate()
    {
        //rigi.position += movingVec * Time.fixedDeltaTime;
        rigi.velocity = new Vector3(movingVec.x, rigi.velocity.y, movingVec.z);
    }
}
