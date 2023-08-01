using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : MonoBehaviour
{
    public float RotateSpeed = 20f;
    private bool isForward = true;
    void Update()
    {
        float angle = transform.eulerAngles.y;
        if (angle > 60 && angle<80 && isForward)
        {
            isForward = false;
            RotateSpeed *= -1;
        }
        else if(angle<300 && angle>280 &&!isForward)
        {
            isForward = true;
            RotateSpeed *= -1;
        }
        transform.RotateAround(transform.position, Vector3.up, RotateSpeed * Time.deltaTime);
    }
}
