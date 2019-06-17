using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float Horizontalspeed=100.0f;
    private GameObject Player;
    private GameObject CameraHandle;
    private float tempEulerX;
    private GameObject model;
    private GameObject camerapos;
    private Vector3 cameravocity;
    // Start is called before the first frame update
    void Awake()
    {
        CameraHandle = transform.parent.gameObject;
        Player = CameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        model = Player.GetComponent<ActorController>().model;
        camerapos = Camera.main.gameObject;
            }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 tempModelEuler = model.transform.eulerAngles;
        Player.transform.Rotate(Vector3.up, pi.jRight * Time.fixedDeltaTime * Horizontalspeed);
        tempEulerX -= pi.jUp * 80.0f * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX,-30,40);
        CameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
        model.transform.eulerAngles= tempModelEuler;

        camerapos.transform.position = Vector3.SmoothDamp(camerapos.transform.position, transform.position, ref cameravocity, 0.1f);
        camerapos.transform.eulerAngles = transform.eulerAngles;
    }
}
