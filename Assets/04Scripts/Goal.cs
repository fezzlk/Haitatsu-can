using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // �S�[���o�[�ɐڐG
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hoge");
        Debug.Log(collision.gameObject.name);
        SceneManager.LoadScene("ResultScene");
    }
}
