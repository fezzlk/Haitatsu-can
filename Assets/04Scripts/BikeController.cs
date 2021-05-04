using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{
    /**  個々の車軸の情報 **/
    public List<AxleInfo> axleInfos;

    /** ホイールの最大トルク **/
    public float maxMotorTorque;

    /** ホイールのハンドル最大角度 **/
    public float maxSteeringAngle;

    /** フローティングジョイスティックのオブジェクト **/
    public FixedJoystick fixedJoystick;

    /** ハンドルのオブジェクト（絵的に回転させるため） **/
    public GameObject steeringObj;

    public List<AudioClip> soundList;

    /** 内部変数（表示用）：モーター **/
    [SerializeField] private float _motor;

    /** 内部変数（表示用）：ハンドルの回転角度 **/
    [SerializeField] private float _steering;

    /** 内部変数：モーターの変化 **/
    private float _motorVal;

    /** 内部変数：加速しているか **/
    private bool _isAccel;

    /** 内部変数：ブレーキがかかっているか **/
    private bool _isBrake;

    void Start()
    {
        // 初期化
        _motor = 0;
        _steering = 0;
        _motorVal = 0;
        _isAccel = false;
        _isBrake = false;
    }

    void Update()
    {
    }
    public void FixedUpdate()
    {
        /**
        * 引用："Updateは毎フレーム呼ばれるのに対し、FixedUpdateは設定されている
        * 一定秒数ごとによばれるというものです。"
        * 毎フレーム呼ぶと処理が重いから？こうしてる？
        **/

        // ジョイスティックの入力がある時
        if (fixedJoystick.Vertical != 0 || fixedJoystick.Horizontal != 0)
        {

            // モーター = モーターの最大トルク × ジョイスティックの縦方向（-1.0 ~ +1.0）
            _motor = maxMotorTorque * fixedJoystick.Vertical;

            // ステアリング = ステアリングの最大旋回角 × ジョイスティックの横方向（-1.0 ~ +1.0）
            _steering = maxSteeringAngle * fixedJoystick.Horizontal;
        }
        // ジョイスティックの入力がない時（キーボード入力）
        else
        {
            // モーター = モーターの最大トルク × キー入力の縦方向（-1.0 ~ +1.0）
            _motor = maxMotorTorque * Input.GetAxis("Vertical");

            // ステアリング = ステアリングの最大旋回角 × キー入力の横方向（-1.0 ~ +1.0）
            _steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        }


        // 各軸ごとに処理を行なう。
        // 今回の場合、バイクであるがプログラム上は四輪であり、
        // 前輪ペアと後輪ペアの2軸をそれぞれ処理する。
        foreach (AxleInfo axleInfo in axleInfos)
        {
            // ハンドル
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = _steering;
                axleInfo.rightWheel.steerAngle = _steering;
            }

            // モーター
            if (axleInfo.motor)
            {

                axleInfo.leftWheel.motorTorque = _motor;
                axleInfo.rightWheel.motorTorque = _motor;
            }
        }

        // ハンドルの描写
        // z成分を3倍にしているのは実体に対して見た目の変化が小さかったから
        Transform visualWheel = steeringObj.transform;
        visualWheel.transform.localEulerAngles = new Vector3(
            visualWheel.localEulerAngles.x,
             visualWheel.localEulerAngles.y,
               _steering * 3);

        // サウンド（エンジン音およびブレーキ音）を作る
        SoundCreator();

    }

    /// <summary>
    /// エンジン音およびブレーキ音を出すメソッド
    /// </summary>
    private void SoundCreator()
    {
        // 以下エンジン音およびブレーキ音

        // モーター>0 （すなわち前進）かつ 加速している かつ 加速フラグがfalse
        if (_motor > 0 && Mathf.Abs(_motorVal) < Mathf.Abs(_motor) && !_isAccel)
        {
            Debug.Log("Engine Start.");

            // エンジン音再生
            GetComponent<AudioSource>().PlayOneShot(soundList[0]);

            // 加速フラグを立てる（もう一度呼ばれないようにするため）
            _isAccel = true;

            // ブレーキフラグを下ろす
            _isBrake = false;

            // モーター変化量を更新する
            _motorVal = _motor;
            return;
        }

        // 減速している かつ 加速フラグがtrue
        if (Mathf.Abs(_motorVal) > Mathf.Abs(_motor) && _isAccel)
        {
            Debug.Log("Engine Stop.");

            // エンジン音停止
            GetComponent<AudioSource>().Stop();

            // 加速フラグを下ろす（次加速したときにエンジン音を鳴らすため）
            _isAccel = false;

            // ブレーキフラグを下ろす
            _isBrake = false;

            // モーター変化量を更新する
            _motorVal = _motor;
            return;
        }

        // モーター<0（後進している） かつ モーターが逆回転している かつ ブレーキフラグがfalse
        if (_motor < 0 && Mathf.Abs(_motorVal) <= Mathf.Abs(_motor) && !_isBrake)
        {
            Debug.Log("Break Start.");
            // ブレーキ音再生
            GetComponent<AudioSource>().PlayOneShot(soundList[1]);

            // 加速フラグを下ろす（次加速したときにエンジン音を鳴らすため）
            _isAccel = false;

            // ブレーキフラグを立てる（ブレーキ音が鳴り続けないようにするため）
            _isBrake = true;

            // モーター変化量を更新する
            _motorVal = _motor;
            return;
        }

    }


    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor; // このホイールはモーターにアタッチされているかどうか
        public bool steering; // このホイールはハンドルの角度を反映しているかどうか
    }
}