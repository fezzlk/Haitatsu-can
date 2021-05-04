using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{

    public List<AxleInfo> axleInfos; // 個々の車軸の情報
    public float maxMotorTorque; // ホイールの最大トルク
    public float maxSteeringAngle; // ホイールのハンドル最大角度

    public GameObject steeringObj;
    [SerializeField] private float motor;

    [SerializeField] private float steering;


    // Use this for initialization
    void Start()
    {
        // Debug.Log("CarStart");
        motor = 0;
        steering = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void FixedUpdate()
    {

        // Debug.Log("CarFUpdate");
        motor = maxMotorTorque * Input.GetAxis("Vertical");
        steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        Debug.Log(Input.GetAxis("Vertical"));
        Debug.Log("motor:" + motor + " steering:" + steering);
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    // 音源取得
                    GetComponent<AudioSource>().Play();
                }
                // if (Input.GetKeyUp(KeyCode.UpArrow))
                // {
                //     GetComponent<AudioSource>().Stop();
                // }
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            // ハンドルの描写
            Transform visualWheel = steeringObj.transform;
            visualWheel.transform.localEulerAngles = new Vector3(
                visualWheel.localEulerAngles.x,
                 visualWheel.localEulerAngles.y,
                   steering * 3);
            // ↑ 3倍にしているのは実体に対して見た目の変化が小さかったから
        }

    }



    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        Transform visualWheel = steeringObj.transform;
        //Vector3 position;
        //Quaternion rotation;
        //collider.GetWorldPose(out position, out rotation);
        // visualWheel.transform.position = position;
        visualWheel.transform.localEulerAngles = new Vector3(
            visualWheel.localEulerAngles.x,
             visualWheel.localEulerAngles.y,
               steering * 3);
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