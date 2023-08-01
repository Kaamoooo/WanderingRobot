using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float RotateSpeed=12;
    public float BackgroundChangeInterval = 8f;
    public Transform Pos1;
    public float Pos1Angle1=120;
    public float Pos1Angle2=215;
    public Transform Pos2;
    public float Pos2Angle1;
    public float Pos2Angle2;
    public Transform Pos3;

    private float curPos = 2;
    private Coroutine posChangeCoroutine = null;
    // Update is called once per frame
    void Update()
    {
        if (posChangeCoroutine == null)
        {
            posChangeCoroutine = StartCoroutine(ChangePosCoroutine());
        }
        float angle = transform.eulerAngles.y;
        switch (curPos)
        {
            case 1:
                if (angle<Pos1Angle1||angle>Pos1Angle2) RotateSpeed *= -1;break;
            case 2:
                if(angle<Pos2Angle1||angle>Pos2Angle2) RotateSpeed *= -1; break;
            case 3:
                transform.rotation = Pos3.rotation; break;
        }
        transform.RotateAround(transform.position, Vector3.up, RotateSpeed * Time.deltaTime);
    }

    IEnumerator ChangePosCoroutine()
    {
        yield return new WaitForSeconds(BackgroundChangeInterval);
        curPos++;
        ChangePos();
        posChangeCoroutine = null;
    }

    void ChangePos()
    {
        switch(curPos)
        {
            case 1:
                transform.position = Pos1.position;
                transform.rotation = Quaternion.LookRotation(Pos1.forward);
                break;
            case 2:
                transform.position = Pos2.position;
                transform.rotation= Quaternion.LookRotation(Pos2.forward);
                break;
            case 3:
                transform.position = Pos3.position;
                transform.rotation=Quaternion.LookRotation(Pos3.forward);
                break;
            default:
                curPos= 1;
                transform.position = Pos1.position;
                transform.rotation=Quaternion.LookRotation(Pos1.forward);
                break;
        }
    }
}
