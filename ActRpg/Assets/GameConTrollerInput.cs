using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class GameConTrollerInput : MonoBehaviour
{
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
    public bool jump = false;
    public bool defenseOn = false;
    public bool defenseOff = true;
    public bool LockOn = false;
    protected bool lastjump;
    public bool attack;
    public bool shiedHit;
    protected bool lastattack;
    [Header("Other")]
    public bool inputEnabled;
    public float MouseSensibility = 1.0f;

    protected float targetDup;
    protected float targetDright;

    protected float velocityDup;
    protected float velocityDright;
    // Start is called before the first frame update
    
    
}
