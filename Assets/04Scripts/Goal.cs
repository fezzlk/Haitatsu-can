using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    private void Start()
    {
        Debug.Log("fuga");
    }

    // ゴールバーを通過した時
    void OnTriggerEnter(Collider collider)
    {
        // 通過したのがバイクであればリザルト画面へ
        if (collider.gameObject.name == "MyCollider")
        {
            Debug.Log("finish!");
            SceneManager.LoadScene("ArrivalScene");
        }
    }
}
