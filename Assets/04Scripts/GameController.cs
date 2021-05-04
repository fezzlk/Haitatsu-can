using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    /** �o�C�N�̃R���g���[���[ **/
    private Vehicle01_Controller bike;
    void Start()
    {
        // �o�C�N�̃R���g���[���[
        bike = GetComponent<Vehicle01_Controller>();
    }

    void Update()
    {

        // Type '��' or 'w' key
        if (Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.W))
        {
            // �O�ɐi��
            bike.GoAhead();
        }

        // Type '��' or 's' key
        if (Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.S))
        {
            // �o�b�N����
            bike.GoBack();
        }

        // Type '��' or 'd' key
        if (Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D))
        {
            // �o�C�N�̊p�x���E�ɋȂ���
            bike.TurnRight();
        }

        // Type '��' or 'a' key
        if (Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A))
        {

            // �o�C�N�̊p�x�����ɋȂ���
            bike.TurnLeft();
        }
    }
}
