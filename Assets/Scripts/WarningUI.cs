using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningUI : MonoBehaviour
{
    public GameObject WarningIconObj;
    public GameObject WarningEffectImageObj ;
    public float MinWarningDistance = 10f;

    public float MaxAlpha = 45f;
    public float AlphaAlterSpeed= 25f;

    private Image warningEffectImage;
    private float curAlpha = 0f;
    private bool revealing = true;

    private void Start()
    {
        warningEffectImage = WarningEffectImageObj.GetComponent<Image>();
    }
    void Update()
    {
        if(GameController.IsWarning==true)
        {
            if (WarningEffectImageObj.activeSelf == false)
            {
                WarningIconObj.SetActive(true);
                WarningEffectImageObj.SetActive(true);
            }
            if(curAlpha<=0)
            {
                revealing = true;
            }
            else if(curAlpha>MaxAlpha)
            {
                revealing=false;
            }
            if(revealing)
            {
                curAlpha += Time.deltaTime * AlphaAlterSpeed;
            }
            else
            {
                curAlpha-=Time.deltaTime*AlphaAlterSpeed;
            }
            warningEffectImage.color = new Color(1, 1, 1, curAlpha);
        }
        else
        {
            if (WarningEffectImageObj.activeSelf)
            {
                Reset();
            }
        }
    }

    private void Reset()
    {
        curAlpha = 0f;
        revealing = true;
        WarningEffectImageObj.SetActive(false);
        WarningIconObj.SetActive(false);
    }
}
