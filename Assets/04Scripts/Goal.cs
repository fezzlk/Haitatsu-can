using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // ゴールバーに接触
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hoge");
        Debug.Log(collision.gameObject.name);
        SceneManager.LoadScene("ResultScene");
    }
}
