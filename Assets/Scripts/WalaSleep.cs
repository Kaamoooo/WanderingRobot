using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalaSleep : MonoBehaviour
{
    public bool IsSleeping= false;
    public Material ScanEffect;
    public float BlackingSpeed = 0.7f;

    private float curBrightness = 1;
    private void Update()
    {
        if (Input.GetButtonDown("Sleep"))
        {
            IsSleeping = !IsSleeping;
        }
        if (GameController.hasBeenCaptured)
        {
            IsSleeping = false;
        }
        if(IsSleeping)
        {
            curBrightness -= Time.deltaTime * BlackingSpeed;
            if (curBrightness <0)
            {
                curBrightness = 0;
            }
        }
        else
        {
            curBrightness+=Time.deltaTime * BlackingSpeed;
            if(curBrightness > 1)
            {
                curBrightness = 1;
            }
        }
        ScanEffect.SetFloat("_Brightness",curBrightness);


    }

}
