using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float Horizontalspeed=100.0f;
    public Image lockDot;
    public bool lockState;
    public Vector3 lockDirection;

    private GameObject Player;
    private GameObject CameraHandle;
    private float tempEulerX;
    private GameObject model;
    private GameObject camerapos;
    private Vector3 cameravocity;
    private LockTerget lockTarget;


    // Start is called before the first frame update
    void Awake()
    {
        CameraHandle = transform.parent.gameObject;
        Player = CameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        model = Player.GetComponent<ActorController>().model;
        camerapos = Camera.main.gameObject;
        lockDot.enabled = false;
        lockState = false;
        Cursor.lockState = CursorLockMode.Locked;
            }
    private void Update()
    {
        if (lockTarget != null)
        {
            lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position);
        
            if (Vector3.Distance(model.transform.position,lockTarget.obj.transform.position)>10.0f)
            {
                 lockTarget = null;
               
                lockDot.enabled = false;
                lockState = false;
            }
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (lockTarget==null)
        {


            Vector3 tempModelEuler = model.transform.eulerAngles;
            Player.transform.Rotate(Vector3.up, pi.jRight * Time.fixedDeltaTime * Horizontalspeed);
            tempEulerX -= pi.jUp * 80.0f * Time.fixedDeltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, -30, 40);
            CameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            //CameraHandle.transform.LookAt(LockTarget.transform);
            Vector3 tempForward = lockTarget.obj.transform.position - transform.position;
            lockDirection= lockTarget.obj.transform.position - model.transform.position;
            lockDirection.y = 0;
            tempForward.y = 0;
            CameraHandle.transform.forward = tempForward;
            CameraHandle.transform.LookAt(lockTarget.obj.transform);
        }
        

        camerapos.transform.position = Vector3.SmoothDamp(camerapos.transform.position, transform.position, ref cameravocity, 0.1f);
        //camerapos.transform.eulerAngles = transform.eulerAngles;
        camerapos.transform.LookAt(CameraHandle.transform);
    }
    public void lockUNLock()
    {
        
            Vector3 medolOrigin = model.transform.position;
            Vector3 modleOrigin1 = medolOrigin + new Vector3(0, 1, 0);
            Vector3 boxCenter = modleOrigin1 + model.transform.forward * 5.0f;
            Collider[] col = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5.0f),model.transform.rotation,LayerMask.GetMask("enemy"));
            
            if (col.Length==0)
            {
                
                lockTarget= null;
            }
            else
            {
                foreach (var item in col)
                {
                    if (lockTarget!=null && lockTarget.obj==item.gameObject)
                    {
                        lockTarget = null;
                        lockDot.enabled = false;                        
                        lockState = false;
                        break;
                        
                    }
                lockTarget= new LockTerget(item.gameObject);
                    lockDot.enabled = true;
                    lockState = true;
                    

                    break;
                }
            }
     
    }
    public class LockTerget
    {
        public GameObject obj;
        public float halfHight;
        public LockTerget(GameObject tobj)
        {
            obj = tobj;
        }
    }
}
