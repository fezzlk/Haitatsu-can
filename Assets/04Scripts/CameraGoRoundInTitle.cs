using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraGoRoundInTitle : MonoBehaviour
{
    public GameObject camera1;

    public float defDeg;

    private float rotDeg;


    // Start is called before the first frame update
    void Start()
    {
        rotDeg = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rotDeg = rotDeg + defDeg;
        camera1.transform.localEulerAngles = new Vector3(
            camera1.transform.localEulerAngles.x,
            rotDeg,
             camera1.transform.localEulerAngles.z
             );
    }
}
