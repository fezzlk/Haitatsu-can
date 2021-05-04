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

    /** �����x�i1��̃A�N�Z���łǂ̂��炢�������邩�j **/
    public float ACCELERATION = 10.0f;

    /** ������i���R�I�Ɍ�������ʁj **/
    public float DECELERATION = 2.0f;

    /** 1��̐���ŉ��p **/
    public float DIF_TURN_ANGLE = 1.0f;

    /** ����p�̍ő�l **/
    public float MAX_TURN_ANGLE = 5.0f;

    /** ���x�̍ő�l **/
    public float MAX_SPEED = 100f;

    /** ������i�����Ȃ�o�b�N�j **/
    [SerializeField] private float _accel = 0.0f;

    /** �o�C�N�������Ă������(deg) **/
    [SerializeField] private float _angle;

    /** �o�C�N�������Ă������(3����) **/
    [SerializeField] private Vector3 _dir;

    /** �n���h���������Ă������ **/
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

        // ��������
        Decelerate();

        // �n���h�������X�ɂ܂������ɂ���
        Linearise();

        // y�����ixz���ʁj��̊p�x���擾����
        _angle = transform.localEulerAngles.y;

        // ���݁A�o�C�N�������Ă���������擾����
        _dir = new Vector3(-Mathf.Sin(GetRadians(_angle)), 0.0f, -Mathf.Cos(GetRadians(_angle)));

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
        _accel = _accel + ACCELERATION;

        // �ő�l�𒴂������͍ő�l�ɂ���
        if (Mathf.Abs(_accel) >= MAX_SPEED)
        {
            // ���݂̉����x�̕���
            float sign = _accel / Mathf.Abs(_accel);
            _accel = MAX_SPEED * sign;
        }

        // Debug.Log("direction of bike:" + _angle + " deg. (" + (_dir.x) + "," + (_dir.z) + ")");

        // ��������͕͂�����
        Vector3 force = new Vector3(-_dir.x, 0.0f, -_dir.z);
        // Debug.Log("force:" + (force.x) + "," + (force.z) + ")");

        // �͂�������
        _rb.AddForce(force * _accel, ForceMode.Acceleration);

    }

    /// <summary>
    /// �o�b�N����B
    /// </summary>
    public void GoBack()
    {
        Debug.Log("Go Back.");

        // �������ɉ���������
        _accel = _accel - ACCELERATION;

        // �ő�l�𒴂������͍ő�l�ɂ���
        if (Mathf.Abs(_accel) >= MAX_SPEED)
        {
            // ���݂̉����x�̕���
            float sign = _accel / Mathf.Abs(_accel);
            _accel = MAX_SPEED * sign;
        }

        Debug.Log("direction of bike:" + _angle + " deg. (" + (_dir.x) + "," + (_dir.z) + ")");

        Vector3 force = new Vector3(-_dir.x, 0.0f, -_dir.z);
        Debug.Log("force:" + (force.x) + "," + (force.z) + ")");

        // �͂�������i������������������ɂ��邽�߁A�����ɂ���B�j
        _rb.AddForce(force * _accel, ForceMode.Acceleration);

    }
    /// <summary>
    /// �E�ɐ��񂷂�
    /// </summary>
    public void TurnRight()
    {
        Debug.Log("Turn Right.");

        Turn(+DIF_TURN_ANGLE);
    }

    /// <summary>
    /// ���ɐ��񂷂�
    /// </summary>
    public void TurnLeft()
    {
        Debug.Log("Turn Left.");

        Turn(-DIF_TURN_ANGLE);
    }

    /// <summary>
    /// �����Ŏw�肵�����������񂷂�
    /// </summary>
    /// <param name="turnAngle">����p</param>
    private void Turn(float turnAngle)
    {
        // ����.����p���������񂷂�
        _steelingAngle = _steelingAngle + turnAngle;

        // �ő�l�𒴂��Ȃ��悤�ɂ���
        if (Mathf.Abs(_steelingAngle) > MAX_TURN_ANGLE)
        {
            // ���݂̐���p�̕���
            float sign = _steelingAngle / Mathf.Abs(_steelingAngle);
            _accel = MAX_TURN_ANGLE * sign;
        }

        // �o�C�N����񂷂�
        transform.Rotate(new Vector3(0, _steelingAngle, 0));
    }


    /// <summary>
    /// ����������B�i�����x�̐�Βl������������B�j
    /// </summary>
    private void Decelerate()
    {

        // �����x�̐�Βl������臒l��菬�����ꍇ������͂ق�0�Ȃ̂�0�ɋߎ�����B
        if (Mathf.Abs(_accel) < DECELERATION)
        {
            // 0�ɂ���
            _accel = 0.0f;

            // �ȉ��͓ǂ܂Ȃ�
            return;
        }

        // ���݂̉����x�̕���
        float sign = _accel / Mathf.Abs(_accel);

        // �����i�����x�̐�Βl������������j
        _accel = (Mathf.Abs(_accel) - DECELERATION) * sign;

    }

    /// <summary>
    /// Steeling�����X�ɐ^�������ɖ߂��B
    /// </summary>
    private void Linearise()
    {
        // �n���h���̌������}1.0�ȉ��ł����0�ɋߎ�����
        if (Mathf.Abs(_steelingAngle) < 1.0f)
        {
            // 0�ɋߎ�
            _steelingAngle = 0.0f;
            return;
        }

        // �n���h���̌����𔼕��ɂ���
        _steelingAngle = _steelingAngle / 2;

    }
    /// <summary>
    /// degree(360��)�����W�A��(2��)�ɕϊ�����
    /// </summary>
    /// <param name="deg">degree</param>
    /// <returns></returns>
    private float GetRadians(float deg)
    {
        return (float)(deg / 360.0f * (2 * Math.PI));
    }
}
