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
    private GameObject Player;
    private GameObject CameraHandle;
    private float tempEulerX;
    private GameObject model;
    private GameObject camerapos;
    private Vector3 cameravocity;
    private GameObject LockTarget=null;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (LockTarget==null)
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
            Vector3 tempForward = LockTarget.transform.position - transform.position;
            tempForward.y = 0;
            CameraHandle.transform.forward = tempForward;

        }
        

        camerapos.transform.position = Vector3.SmoothDamp(camerapos.transform.position, transform.position, ref cameravocity, 0.1f);
        //camerapos.transform.eulerAngles = transform.eulerAngles;
        camerapos.transform.LookAt(CameraHandle.transform);
    }
    public void lockUNLock()
    {
        
            Vector3 medolOrigin = model.transform.position;
            Vector3 modleOrigin1 = medolOrigin + new Vector3(0, 1, 0);
            Vector3 boxCenter = modleOrigin1 + transform.forward * 5.0f;
            Collider[] col = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5.0f),model.transform.rotation,LayerMask.GetMask("enemy"));
            if (col.Length==0)
            {
                
                LockTarget = null;
            }
            else
            {
                foreach (var item in col)
                {
                    if (LockTarget==item.gameObject)
                    {
                        LockTarget = null;
                        lockDot.enabled = false;
                        print("clear" + LockTarget);
                        lockState = false;
                        break;
                        
                    }
                    LockTarget =item.gameObject;
                    lockDot.enabled = true;
                    lockState = true;
                    print("锁定" + LockTarget);

                    break;
                }

            }
            
        
    }
}
