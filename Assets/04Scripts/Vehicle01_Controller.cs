using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle01_Controller : MonoBehaviour
{
    // [Header("�����鎞��"), Range(0.0625f, 30)]
    public float maxSpead; //�ő呬�x
    public float minSpeed; //�Œᑬ�x
    public float accel; //����
    private static float speed = 0; //���݂̃X�s�[�h

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
        // Type '��' or 'w' key
        if (Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.W))
        {
            // position�X�V�ɂ��ړ��͈�U�R�����g�A�E�g
            // if (maxSpead >= speed)
            // {
            // Accelerate
            // speed = speed + accel;
            // }

            // ���g�̌����x�N�g���擾
            // ���͂Ŏ��������l�������̂Ŕq��
            // http://ftvoid.com/blog/post/631
            float angleDir = transform.eulerAngles.z * (Mathf.PI / 180.0f);
            Vector3 dir = new Vector3(Mathf.Cos(angleDir), Mathf.Sin(angleDir), 0.0f);

            rb.AddForce(dir * power, ForceMode.Acceleration);
        }

        // Type '��' or 's' key
        if (Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.S))
        {
            // position�X�V�ɂ��ړ��͈�U�R�����g�A�E�g
            // if (minSpeed <= speed)
            // {
            // Accelerate
            // speed = speed - accel;
            // }
        }

        // Type '��' or 'd' key
        if (Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D))
        {
            // Todo: �o�C�N�̊p�x���E�ɋȂ���
        }

        // Type '��' or 'a' key
        if (Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A))
        {
            // Todo: �o�C�N�̊p�x�����ɋȂ���
        }

        // �ړ��iposition�̍X�V�j
        // this.transform.position += transform.forward * speed * Time.deltaTime;
    }
}
