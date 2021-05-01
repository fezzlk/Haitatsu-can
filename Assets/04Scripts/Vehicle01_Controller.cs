using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Vehicle01_Controller : MonoBehaviour
{
    // [Header("かかる時間"), Range(0.0625f, 30)]
    //public float maxSpeed; //最大速度
    //public float minSpeed; //最低速度
    //public float accel; //加速
    //private static float speed = 0; //現在のスピード

    Rigidbody rb;

    /** 加速具合（負数ならバック） **/
    [SerializeField] private float _accel = 0.0f;

    /** バイクが向いている方向(deg) **/
    [SerializeField] private float _angle;

    /** バイクが向いている方向(3成分) **/
    [SerializeField] private Vector3 _dir;

    /** ハンドルが向いている向き **/
    [SerializeField] private float _steelingAngle = 0.0f;

    void Start()
    {
        // Get RigidBody of Bike
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // スピードを落とす
        if (_accel > 2.0)
        {
            _accel -= 2.0f;
        }
        else if (_accel < -2.0)
        {
            _accel += 2.0f;
        }
        else
        {
            _accel = 0.0f;
        }

        _steelingAngle = _steelingAngle / 2;

        // y方向（xz平面）上の角度を取得する
        _angle = transform.localEulerAngles.y;

        // 現在、バイクが向いている方向を取得する
        _dir = new Vector3(-Mathf.Sin(GetRadians(_angle)), 0.0f, -Mathf.Cos(GetRadians(_angle)));

        // Type '↑' or 'w' key
        if (Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.W))
        {
            // 前に進む
            GoAhead();
        }

        // Type '↓' or 's' key
        if (Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.S))
        {
            // バックする
            GoBack();
        }

        // Type '→' or 'd' key
        if (Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D))
        {
            // Todo: バイクの角度を右に曲げる
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 10.0f, transform.eulerAngles.z);
            _steelingAngle += +1.0f;
            if (_steelingAngle >= 15)
            {
                _steelingAngle = 15.0f;
            }

            // rb.AddTorque(Vector3.up * _steelingAngle * (2 * Mathf.PI), ForceMode.Acceleration);

        }


        // Type '←' or 'a' key
        if (Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A))
        {

            // Todo: バイクの角度を左に曲げる

            _steelingAngle -= 1.0f;
            if (_steelingAngle <= -15)
            {
                _steelingAngle = -15.0f;
            }

            // transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 10.0f, transform.eulerAngles.z);
            //rb.AddTorque(Vector3.up * -15 * (2 * Mathf.PI), ForceMode.Acceleration);
        }

        transform.Rotate(new Vector3(0, _steelingAngle, 0));
        //rb.AddTorque(Vector3.up * _steelingAngle * Mathf.PI, ForceMode.Acceleration);
        // 移動（positionの更新）
        // this.transform.position += transform.forward * speed * Time.deltaTime;
    }

    /// <summary>
    /// 前に進む。
    /// </summary>
    public void GoAhead()
    {
        Debug.Log("Go Ahead.");
        // position更新による移動は一旦コメントアウト
        // if (maxSpead >= speed)
        // {
        // Accelerate
        // speed = speed + accel;
        // }

        // 自身の向きベクトル取得
        // 自力で実装した人がいたので拝借
        // http://ftvoid.com/blog/post/631

        // 加速させる
        _accel += 10.0f;
        if (_accel >= 100)
        {
            _accel = 100.0f;
        }

        // y方向（xz平面）上の角度を取得する
        //float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f);

        // 現在、バイクが向いている方向を取得する
        //Vector3 dir = new Vector3(Mathf.Cos(angleDir), 0.0f, Mathf.Sin(angleDir));
        Debug.Log("direction of bike:" + _angle + " deg. (" + (_dir.x) + "," + (_dir.z) + ")");

        Vector3 force = new Vector3(-_dir.x, 0.0f, -_dir.z);
        Debug.Log("force:" + (force.x) + "," + (force.z) + ")");

        // 力を加える（ただし引っ張る方向にするため、負数にする。）
        rb.AddForce(force * _accel, ForceMode.Acceleration);

    }

    /// <summary>
    /// バックする
    /// </summary>
    public void GoBack()
    {
        Debug.Log("Go Back.");

        // 減速させる
        _accel -= 10.0f;
        if (_accel <= -100)
        {
            _accel = -100.0f;
        }

        // y方向（xz平面）上の角度を取得する
        //float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f);

        // 現在、バイクが向いている方向を取得する
        //Vector3 dir = new Vector3(Mathf.Cos(angleDir), 0.0f, Mathf.Sin(angleDir));
        //Debug.Log("direction of bike:" + dir);

        Debug.Log("direction of bike:" + _angle + " deg. (" + (_dir.x) + "," + (_dir.z) + ")");

        Vector3 force = new Vector3(-_dir.x, 0.0f, -_dir.z);
        Debug.Log("force:" + (force.x) + "," + (force.z) + ")");

        // 力を加える（ただし引っ張る方向にするため、負数にする。）
        rb.AddForce(force * _accel, ForceMode.Acceleration);

    }

    /// <summary>
    /// degree(360°)をラジアン(2π)に変換する
    /// </summary>
    /// <param name="deg"></param>
    /// <returns></returns>
    private float GetRadians(float deg)
    {
        return (float)(deg / 360.0f * (2 * Math.PI));
    }
}
