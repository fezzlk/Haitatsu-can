using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBikeFromSky : MonoBehaviour
{
    // public static int cameraOffsetX = 10;
    [SerializeField] private GameObject bike;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        var pos = bike.transform.position;
        // pos.x += cameraOffsetX;
        pos.y = transform.position.y;
        transform.position = pos;
    }
}
