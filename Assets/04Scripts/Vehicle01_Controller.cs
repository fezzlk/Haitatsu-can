using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle01_Controller : MonoBehaviour
{

    public float maxSpead; //最大速度
    public float minSpeed; //最低速度
    public float accel; //加速
    private static float speed = 0; //現在のスピード


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Type '↑' or 'w' key
        if (Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.W))
        {
            if (maxSpead >= speed)
            {
                // Accelerate
                speed = speed + accel;
            }
        }

        // Type '↓' or 's' key
        if (Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.S))
        {
            if (minSpeed <= speed)
            {
                // Accelerate
                speed = speed - accel;
            }
        }
        this.transform.position += transform.forward * speed * Time.deltaTime;
    }
}
