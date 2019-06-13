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

    [Header("Output signal")]
    public float Dup;
    public float Dright;
    public bool run = false;
    public float Dmag;
    public Vector3 Dvec;
    [Header("Other")]
    public bool inputEnabled = true;


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

        if (inputEnabled==false)
        {
            targetDright = 0;
            targetDup = 0;
        }
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);//调用SmoothDamp对上下旗帜进行衰减处理，移动更平滑。
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);
        Dmag = Mathf.Sqrt((Dup * Dup) + (Dright * Dright));
        Dvec = Dright * transform.right + Dup * transform.forward;

        run = Input.GetKey(KeyA);
    }
}
