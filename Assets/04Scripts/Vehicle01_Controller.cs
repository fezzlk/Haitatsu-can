using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle01_Controller : MonoBehaviour
{

    public float maxSpead; //�ő呬�x
    public float minSpeed; //�Œᑬ�x
    public float accel; //����
    private static float speed = 0; //���݂̃X�s�[�h


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Type '��' or 'w' key
        if (Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.W))
        {
            if (maxSpead >= speed)
            {
                // Accelerate
                speed = speed + accel;
            }
        }

        // Type '��' or 's' key
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
