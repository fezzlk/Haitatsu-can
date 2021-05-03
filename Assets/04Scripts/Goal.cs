using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // ƒS[ƒ‹ƒo[‚ÉÚG
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hoge");
        Debug.Log(collision.gameObject.name);
        SceneManager.LoadScene("ResultScene");
    }
}
