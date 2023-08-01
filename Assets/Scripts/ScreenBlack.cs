using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBlack : MonoBehaviour
{
    public float BlackTime = 8f;

    private Image image;
    private bool startBlacking=false;
    private Coroutine blackCntCoroutine;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
        if(GameController.hasBeenCaptured)
        {
            if(blackCntCoroutine==null)blackCntCoroutine=StartCoroutine(SetStartBlacking(true, 2f));
            if (startBlacking)
            {
                image.color = new Color(0, 0, 0, image.color.a + Time.deltaTime / BlackTime);
                if (image.color.a >= 1)
                {
                    GameController.EscapeFailed();
                    //Gameover
                }
            }
        }
        else if(startBlacking)
        {
            Reset();
        }
    }
    private void Reset()
    {
        image.color = new Color(0, 0, 0, 0);
        blackCntCoroutine = null;
        startBlacking = false;
    }
    private IEnumerator SetStartBlacking(bool setBlacking, float delay)
    {
        yield return new WaitForSeconds(delay);
        startBlacking = setBlacking;
    }
}
