using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    static float countTime = 0;
    static bool isActive = true;
    public static float getTime()
    {
        return countTime;
    }

    public static void resetTime()
    {
        countTime = 0;
    }

    public static void setIsActive(bool flag)
    {
        isActive = flag;
    }

    void Start()
    {

    }

    void Update()
    {
        if (isActive)
        {
            countTime += Time.deltaTime;
            GetComponent<Text>().text = countTime.ToString("F2");
        }
    }
}