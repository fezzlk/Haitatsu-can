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

    /** 加速度（1回のアクセルでどのくらい加速するか） **/
    public float ACCELERATION = 10.0f;

    /** 減速具合（自然的に減速する量） **/
    public float DECELERATION = 2.0f;

    /** 1回の旋回で回る角 **/
    public float DIF_TURN_ANGLE = 1.0f;

    /** 旋回角の最大値 **/
    public float MAX_TURN_ANGLE = 5.0f;

    /** 速度の最大値 **/
    public float MAX_SPEED = 100f;

    /** 加速具合（負数ならバック） **/
    [SerializeField] private float _accel = 0.0f;

    /** バイクが向いている方向(deg) **/
    [SerializeField] private float _angle;

    /** バイクが向いている方向(3成分) **/
    [SerializeField] private Vector3 _dir;

    /** ハンドルが向いている向き **/
    [SerializeField] private float _steelingAngle = 0.0f;

    /** RigidBody **/
    private Rigidbody _rb;
    void Start()
    {
        // Get RigidBody of Bike
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        // 減速処理
        Decelerate();

        // ハンドルを徐々にまっすぐにする
        Linearise();

        // y方向（xz平面）上の角度を取得する
        _angle = transform.localEulerAngles.y;

        // 現在、バイクが向いている方向を取得する
        _dir = new Vector3(-Mathf.Sin(GetRadians(_angle)), 0.0f, -Mathf.Cos(GetRadians(_angle)));

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
        _accel = _accel + ACCELERATION;

        // 最大値を超えた時は最大値にする
        if (Mathf.Abs(_accel) >= MAX_SPEED)
        {
            // 現在の加速度の符号
            float sign = _accel / Mathf.Abs(_accel);
            _accel = MAX_SPEED * sign;
        }

        // Debug.Log("direction of bike:" + _angle + " deg. (" + (_dir.x) + "," + (_dir.z) + ")");

        // 引っ張る力は負方向
        Vector3 force = new Vector3(-_dir.x, 0.0f, -_dir.z);
        // Debug.Log("force:" + (force.x) + "," + (force.z) + ")");

        // 力を加える
        _rb.AddForce(force * _accel, ForceMode.Acceleration);

    }

    /// <summary>
    /// バックする。
    /// </summary>
    public void GoBack()
    {
        Debug.Log("Go Back.");

        // 負方向に加速させる
        _accel = _accel - ACCELERATION;

        // 最大値を超えた時は最大値にする
        if (Mathf.Abs(_accel) >= MAX_SPEED)
        {
            // 現在の加速度の符号
            float sign = _accel / Mathf.Abs(_accel);
            _accel = MAX_SPEED * sign;
        }

        Debug.Log("direction of bike:" + _angle + " deg. (" + (_dir.x) + "," + (_dir.z) + ")");

        Vector3 force = new Vector3(-_dir.x, 0.0f, -_dir.z);
        Debug.Log("force:" + (force.x) + "," + (force.z) + ")");

        // 力を加える（ただし引っ張る方向にするため、負数にする。）
        _rb.AddForce(force * _accel, ForceMode.Acceleration);

    }
    /// <summary>
    /// 右に旋回する
    /// </summary>
    public void TurnRight()
    {
        Debug.Log("Turn Right.");

        Turn(+DIF_TURN_ANGLE);
    }

    /// <summary>
    /// 左に旋回する
    /// </summary>
    public void TurnLeft()
    {
        Debug.Log("Turn Left.");

        Turn(-DIF_TURN_ANGLE);
    }

    /// <summary>
    /// 引数で指定した分だけ旋回する
    /// </summary>
    /// <param name="turnAngle">旋回角</param>
    private void Turn(float turnAngle)
    {
        // 引数.旋回角分だけ旋回する
        _steelingAngle = _steelingAngle + turnAngle;

        // 最大値を超えないようにする
        if (Mathf.Abs(_steelingAngle) > MAX_TURN_ANGLE)
        {
            // 現在の旋回角の符号
            float sign = _steelingAngle / Mathf.Abs(_steelingAngle);
            _accel = MAX_TURN_ANGLE * sign;
        }

        // バイクを旋回する
        transform.Rotate(new Vector3(0, _steelingAngle, 0));
    }


    /// <summary>
    /// 減速させる。（加速度の絶対値を小さくする。）
    /// </summary>
    private void Decelerate()
    {

        // 加速度の絶対値が減速閾値より小さい場合→それはほぼ0なので0に近似する。
        if (Mathf.Abs(_accel) < DECELERATION)
        {
            // 0にする
            _accel = 0.0f;

            // 以下は読まない
            return;
        }

        // 現在の加速度の符号
        float sign = _accel / Mathf.Abs(_accel);

        // 減速（加速度の絶対値を小さくする）
        _accel = (Mathf.Abs(_accel) - DECELERATION) * sign;

    }

    /// <summary>
    /// Steelingを徐々に真っ直ぐに戻す。
    /// </summary>
    private void Linearise()
    {
        // ハンドルの向きが±1.0以下であれば0に近似する
        if (Mathf.Abs(_steelingAngle) < 1.0f)
        {
            // 0に近似
            _steelingAngle = 0.0f;
            return;
        }

        // ハンドルの向きを半分にする
        _steelingAngle = _steelingAngle / 2;

    }
    /// <summary>
    /// degree(360°)をラジアン(2π)に変換する
    /// </summary>
    /// <param name="deg">degree</param>
    /// <returns></returns>
    private float GetRadians(float deg)
    {
        return (float)(deg / 360.0f * (2 * Math.PI));
    }
}
