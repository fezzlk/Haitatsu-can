using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    /** バイクのコントローラー **/
    private Vehicle01_Controller bike;
    void Start()
    {
        // バイクのコントローラー
        bike = GetComponent<Vehicle01_Controller>();
    }

    void Update()
    {

        // Type '↑' or 'w' key
        if (Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.W))
        {
            // 前に進む
            bike.GoAhead();
        }

        // Type '↓' or 's' key
        if (Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.S))
        {
            // バックする
            bike.GoBack();
        }

        // Type '→' or 'd' key
        if (Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D))
        {
            // バイクの角度を右に曲げる
            bike.TurnRight();
        }

        // Type '←' or 'a' key
        if (Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A))
        {

            // バイクの角度を左に曲げる
            bike.TurnLeft();
        }
    }
}
