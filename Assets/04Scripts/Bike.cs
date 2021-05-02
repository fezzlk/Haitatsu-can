using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike : MonoBehaviour
{

    public List<AxleInfo> axleInfos; // �X�̎Ԏ��̏��
    public float maxMotorTorque; // �z�C�[���̍ő�g���N
    public float maxSteeringAngle; // �z�C�[���̃n���h���ő�p�x

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

            // �n���h���̔��f�͂��܂��s���Ȃ��̂Ŗ�����
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
        public bool motor; // ���̃z�C�[���̓��[�^�[�ɃA�^�b�`����Ă��邩�ǂ���
        public bool steering; // ���̃z�C�[���̓n���h���̊p�x�𔽉f���Ă��邩�ǂ���
    }
}