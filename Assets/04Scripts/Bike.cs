using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike : MonoBehaviour
{

    public List<AxleInfo> axleInfos; // 個々の車軸の情報
    public float maxMotorTorque; // ホイールの最大トルク
    public float maxSteeringAngle; // ホイールのハンドル最大角度

    public GameObject steering;

    // Use this for initialization
    void Start()
    {
        Debug.Log("CarStart");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FixedUpdate()
    {
        Debug.Log("CarFUpdate");
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
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
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            // ハンドルの反映はうまく行かないので未実装
            //ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            //ApplyLocalPositionToVisuals(axleInfo.leftWheel);
        }

    }



    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        Transform visualWheel = steering.transform;
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        // visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
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