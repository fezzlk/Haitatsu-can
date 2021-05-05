using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButtonActivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ñﬂÇÈÉ{É^ÉìÇ≈èIóπ
        if (Application.platform == RuntimePlatform.Android &&
            (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu)))
        {
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
