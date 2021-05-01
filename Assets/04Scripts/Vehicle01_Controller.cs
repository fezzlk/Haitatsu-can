using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Vehicle01_Controller : MonoBehaviour
{
    // [Header("�����鎞��"), Range(0.0625f, 30)]
    //public float maxSpeed; //�ő呬�x
    //public float minSpeed; //�Œᑬ�x
    //public float accel; //����
    //private static float speed = 0; //���݂̃X�s�[�h

    Rigidbody rb;

    /** ������i�����Ȃ�o�b�N�j **/
    [SerializeField] private float _accel = 0.0f;

    /** �o�C�N�������Ă������(deg) **/
    [SerializeField] private float _angle;

    /** �o�C�N�������Ă������(3����) **/
    [SerializeField] private Vector3 _dir;

    /** �n���h���������Ă������ **/
    [SerializeField] private float _steelingAngle = 0.0f;

    void Start()
    {
        // Get RigidBody of Bike
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // �X�s�[�h�𗎂Ƃ�
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

        // y�����ixz���ʁj��̊p�x���擾����
        _angle = transform.localEulerAngles.y;

        // ���݁A�o�C�N�������Ă���������擾����
        _dir = new Vector3(-Mathf.Sin(GetRadians(_angle)), 0.0f, -Mathf.Cos(GetRadians(_angle)));

        // Type '��' or 'w' key
        if (Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.W))
        {
            // �O�ɐi��
            GoAhead();
        }

        // Type '��' or 's' key
        if (Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.S))
        {
            // �o�b�N����
            GoBack();
        }

        // Type '��' or 'd' key
        if (Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D))
        {
            // Todo: �o�C�N�̊p�x���E�ɋȂ���
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 10.0f, transform.eulerAngles.z);
            _steelingAngle += +1.0f;
            if (_steelingAngle >= 15)
            {
                _steelingAngle = 15.0f;
            }

            // rb.AddTorque(Vector3.up * _steelingAngle * (2 * Mathf.PI), ForceMode.Acceleration);

        }


        // Type '��' or 'a' key
        if (Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A))
        {

            // Todo: �o�C�N�̊p�x�����ɋȂ���

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
        // �ړ��iposition�̍X�V�j
        // this.transform.position += transform.forward * speed * Time.deltaTime;
    }

    /// <summary>
    /// �O�ɐi�ށB
    /// </summary>
    public void GoAhead()
    {
        Debug.Log("Go Ahead.");
        // position�X�V�ɂ��ړ��͈�U�R�����g�A�E�g
        // if (maxSpead >= speed)
        // {
        // Accelerate
        // speed = speed + accel;
        // }

        // ���g�̌����x�N�g���擾
        // ���͂Ŏ��������l�������̂Ŕq��
        // http://ftvoid.com/blog/post/631

        // ����������
        _accel += 10.0f;
        if (_accel >= 100)
        {
            _accel = 100.0f;
        }

        // y�����ixz���ʁj��̊p�x���擾����
        //float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f);

        // ���݁A�o�C�N�������Ă���������擾����
        //Vector3 dir = new Vector3(Mathf.Cos(angleDir), 0.0f, Mathf.Sin(angleDir));
        Debug.Log("direction of bike:" + _angle + " deg. (" + (_dir.x) + "," + (_dir.z) + ")");

        Vector3 force = new Vector3(-_dir.x, 0.0f, -_dir.z);
        Debug.Log("force:" + (force.x) + "," + (force.z) + ")");

        // �͂�������i������������������ɂ��邽�߁A�����ɂ���B�j
        rb.AddForce(force * _accel, ForceMode.Acceleration);

    }

    /// <summary>
    /// �o�b�N����
    /// </summary>
    public void GoBack()
    {
        Debug.Log("Go Back.");

        // ����������
        _accel -= 10.0f;
        if (_accel <= -100)
        {
            _accel = -100.0f;
        }

        // y�����ixz���ʁj��̊p�x���擾����
        //float angleDir = transform.eulerAngles.y * (Mathf.PI / 180.0f);

        // ���݁A�o�C�N�������Ă���������擾����
        //Vector3 dir = new Vector3(Mathf.Cos(angleDir), 0.0f, Mathf.Sin(angleDir));
        //Debug.Log("direction of bike:" + dir);

        Debug.Log("direction of bike:" + _angle + " deg. (" + (_dir.x) + "," + (_dir.z) + ")");

        Vector3 force = new Vector3(-_dir.x, 0.0f, -_dir.z);
        Debug.Log("force:" + (force.x) + "," + (force.z) + ")");

        // �͂�������i������������������ɂ��邽�߁A�����ɂ���B�j
        rb.AddForce(force * _accel, ForceMode.Acceleration);

    }

    /// <summary>
    /// degree(360��)�����W�A��(2��)�ɕϊ�����
    /// </summary>
    /// <param name="deg"></param>
    /// <returns></returns>
    private float GetRadians(float deg)
    {
        return (float)(deg / 360.0f * (2 * Math.PI));
    }
}
