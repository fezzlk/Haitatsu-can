using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle01_Controller : MonoBehaviour
{
    // [Header("かかる時間"), Range(0.0625f, 30)]
    public float maxSpead; //最大速度
    public float minSpeed; //最低速度
    public float accel; //加速
    private static float speed = 0; //現在のスピード

    Rigidbody rb;
    float power = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Get RigitBody of Bike
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Type '↑' or 'w' key
        if (Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.W))
        {
            // position更新による移動は一旦コメントアウト
            // if (maxSpead >= speed)
            // {
            // Accelerate
            // speed = speed + accel;
            // }

            // 自身の向きベクトル取得
            // 自力で実装した人がいたので拝借
            // http://ftvoid.com/blog/post/631
            float angleDir = transform.eulerAngles.z * (Mathf.PI / 180.0f);
            Vector3 dir = new Vector3(Mathf.Cos(angleDir), Mathf.Sin(angleDir), 0.0f);

            rb.AddForce(dir * power, ForceMode.Acceleration);
        }

        // Type '↓' or 's' key
        if (Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.S))
        {
            // position更新による移動は一旦コメントアウト
            // if (minSpeed <= speed)
            // {
            // Accelerate
            // speed = speed - accel;
            // }
        }

        // Type '→' or 'd' key
        if (Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D))
        {
            // Todo: バイクの角度を右に曲げる
        }

        // Type '←' or 'a' key
        if (Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A))
        {
            // Todo: バイクの角度を左に曲げる
        }

        // 移動（positionの更新）
        // this.transform.position += transform.forward * speed * Time.deltaTime;
    }
}
