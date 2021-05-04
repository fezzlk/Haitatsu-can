using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{
    /**  �X�̎Ԏ��̏�� **/
    public List<AxleInfo> axleInfos;

    /** �z�C�[���̍ő�g���N **/
    public float maxMotorTorque;

    /** �z�C�[���̃n���h���ő�p�x **/
    public float maxSteeringAngle;

    /** �t���[�e�B���O�W���C�X�e�B�b�N�̃I�u�W�F�N�g **/
    public FixedJoystick fixedJoystick;

    /** �n���h���̃I�u�W�F�N�g�i�G�I�ɉ�]�����邽�߁j **/
    public GameObject steeringObj;

    public List<AudioClip> soundList;

    /** �����ϐ��i�\���p�j�F���[�^�[ **/
    [SerializeField] private float _motor;

    /** �����ϐ��i�\���p�j�F�n���h���̉�]�p�x **/
    [SerializeField] private float _steering;

    /** �����ϐ��F���[�^�[�̕ω� **/
    private float _motorVal;

    /** �����ϐ��F�������Ă��邩 **/
    private bool _isAccel;

    /** �����ϐ��F�u���[�L���������Ă��邩 **/
    private bool _isBrake;

    void Start()
    {
        // ������
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
        * ���p�F"Update�͖��t���[���Ă΂��̂ɑ΂��AFixedUpdate�͐ݒ肳��Ă���
        * ���b�����Ƃɂ�΂��Ƃ������̂ł��B"
        * ���t���[���ĂԂƏ������d������H�������Ă�H
        **/

        // �W���C�X�e�B�b�N�̓��͂����鎞
        if (fixedJoystick.Vertical != 0 || fixedJoystick.Horizontal != 0)
        {

            // ���[�^�[ = ���[�^�[�̍ő�g���N �~ �W���C�X�e�B�b�N�̏c�����i-1.0 ~ +1.0�j
            _motor = maxMotorTorque * fixedJoystick.Vertical;

            // �X�e�A�����O = �X�e�A�����O�̍ő����p �~ �W���C�X�e�B�b�N�̉������i-1.0 ~ +1.0�j
            _steering = maxSteeringAngle * fixedJoystick.Horizontal;
        }
        // �W���C�X�e�B�b�N�̓��͂��Ȃ����i�L�[�{�[�h���́j
        else
        {
            // ���[�^�[ = ���[�^�[�̍ő�g���N �~ �L�[���͂̏c�����i-1.0 ~ +1.0�j
            _motor = maxMotorTorque * Input.GetAxis("Vertical");

            // �X�e�A�����O = �X�e�A�����O�̍ő����p �~ �L�[���͂̉������i-1.0 ~ +1.0�j
            _steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        }


        // �e�����Ƃɏ������s�Ȃ��B
        // ����̏ꍇ�A�o�C�N�ł��邪�v���O������͎l�ւł���A
        // �O�փy�A�ƌ�փy�A��2�������ꂼ�ꏈ������B
        foreach (AxleInfo axleInfo in axleInfos)
        {
            // �n���h��
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = _steering;
                axleInfo.rightWheel.steerAngle = _steering;
            }

            // ���[�^�[
            if (axleInfo.motor)
            {

                axleInfo.leftWheel.motorTorque = _motor;
                axleInfo.rightWheel.motorTorque = _motor;
            }
        }

        // �n���h���̕`��
        // z������3�{�ɂ��Ă���͎̂��̂ɑ΂��Č����ڂ̕ω�����������������
        Transform visualWheel = steeringObj.transform;
        visualWheel.transform.localEulerAngles = new Vector3(
            visualWheel.localEulerAngles.x,
             visualWheel.localEulerAngles.y,
               _steering * 3);

        // �T�E���h�i�G���W��������уu���[�L���j�����
        SoundCreator();

    }

    /// <summary>
    /// �G���W��������уu���[�L�����o�����\�b�h
    /// </summary>
    private void SoundCreator()
    {
        // �ȉ��G���W��������уu���[�L��

        // ���[�^�[>0 �i���Ȃ킿�O�i�j���� �������Ă��� ���� �����t���O��false
        if (_motor > 0 && Mathf.Abs(_motorVal) < Mathf.Abs(_motor) && !_isAccel)
        {
            Debug.Log("Engine Start.");

            // �G���W�����Đ�
            GetComponent<AudioSource>().PlayOneShot(soundList[0]);

            // �����t���O�𗧂Ă�i������x�Ă΂�Ȃ��悤�ɂ��邽�߁j
            _isAccel = true;

            // �u���[�L�t���O�����낷
            _isBrake = false;

            // ���[�^�[�ω��ʂ��X�V����
            _motorVal = _motor;
            return;
        }

        // �������Ă��� ���� �����t���O��true
        if (Mathf.Abs(_motorVal) > Mathf.Abs(_motor) && _isAccel)
        {
            Debug.Log("Engine Stop.");

            // �G���W������~
            GetComponent<AudioSource>().Stop();

            // �����t���O�����낷�i�����������Ƃ��ɃG���W������炷���߁j
            _isAccel = false;

            // �u���[�L�t���O�����낷
            _isBrake = false;

            // ���[�^�[�ω��ʂ��X�V����
            _motorVal = _motor;
            return;
        }

        // ���[�^�[<0�i��i���Ă���j ���� ���[�^�[���t��]���Ă��� ���� �u���[�L�t���O��false
        if (_motor < 0 && Mathf.Abs(_motorVal) <= Mathf.Abs(_motor) && !_isBrake)
        {
            Debug.Log("Break Start.");
            // �u���[�L���Đ�
            GetComponent<AudioSource>().PlayOneShot(soundList[1]);

            // �����t���O�����낷�i�����������Ƃ��ɃG���W������炷���߁j
            _isAccel = false;

            // �u���[�L�t���O�𗧂Ă�i�u���[�L�����葱���Ȃ��悤�ɂ��邽�߁j
            _isBrake = true;

            // ���[�^�[�ω��ʂ��X�V����
            _motorVal = _motor;
            return;
        }

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