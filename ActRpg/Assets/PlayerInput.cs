using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Key Setting")]
    public string KeyUp = "w";
    public string KeyDown = "s";
    public string KeyRight = "d";
    public string KeyLeft = "a";
    public string KeyA;
    public string KeyB;
    public string KeyC;
    public string KeyD;
    public string keyE;
    public string keyF;
    public float Dup;
    public float Dright;
    

    public float Dmag;
    public float jUp;
    public float jRight;
    public Vector3 Dvec;
    public string RKeyUp;
    public string RKeyDown;
    public string RKeyRight;
    public string RKeyLeft;
    [Header("Output signal")]
    
    public bool run = false;
    public bool jump=false;
    public bool defenseOn = false;
    public bool defenseOff = true;
    public bool LockOn = false;
    private bool lastjump;
    public bool attack = false;
    public bool shiedHit=false;
    private bool lastattack;
    [Header("Other")]
    public bool inputEnabled = true;
    public float MouseSensibility=1.0f;



    private float targetDup;
    private float targetDright;
    
    private float velocityDup;
    private float velocityDright;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetDup = (Input.GetKey(KeyUp) ? 1.0f : 0) - (Input.GetKey(KeyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(KeyRight) ? 1.0f : 0) - (Input.GetKey(KeyLeft) ? 1.0f : 0);
        //jUp = (Input.GetKey(RKeyUp) ? 1.0f : 0) - (Input.GetKey(RKeyDown) ? 1.0f : 0);
        //jRight = (Input.GetKey(RKeyRight) ? 1.0f : 0) - (Input.GetKey(RKeyLeft) ? 1.0f : 0);
        jUp = Input.GetAxis("Mouse Y")*MouseSensibility;
        jRight = Input.GetAxis("Mouse X")*MouseSensibility;
        if (inputEnabled==false)
        {
            targetDright = 0;
            targetDup = 0;
        }
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);//调用SmoothDamp对上下旗帜进行衰减处理，移动更平滑。
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);
        Vector2 tempDaxis = SquareToCircle(new Vector2(Dup, Dright));
        float Dup2 = tempDaxis.x;
        float Dright2 = tempDaxis.y;

        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright2 * transform.right + Dup2 *transform.forward;
        LockOn = Input.GetKeyDown(keyE);
        
        run = Input.GetKey(KeyA);

        shiedHit = Input.GetKeyDown(keyF);

        defenseOn = Input.GetKeyDown(KeyD);
        defenseOff = Input.GetKeyUp(KeyD);
        bool newjump = Input.GetKey(KeyB);
        
        if (newjump!=lastjump&&newjump==true)
        {
            jump = true;
           
        }
        else
        {
            jump = false;
        }
        lastjump = newjump;

        bool newattack = Input.GetKey(KeyC);

        if (newattack != lastattack && newattack == true)
        {
            attack = true;

        }
        else
        {
            attack = false;
        }
        lastattack = newattack;
    }
    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
        
    }

}
