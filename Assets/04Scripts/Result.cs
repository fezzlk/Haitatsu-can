using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Result : MonoBehaviour
{
    private TextMeshProUGUI scoreText = null;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponent<TextMeshProUGUI>());
        scoreText = GetComponent<TextMeshProUGUI>();
        if (scoreText != null)
        {

            scoreText.text = string.Format("Your Score : {0} sec", Timer.getTime());
        }
        // scoreText.text = ;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
