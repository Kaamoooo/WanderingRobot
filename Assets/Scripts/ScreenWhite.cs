using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenWhite : MonoBehaviour
{
    public float WhiteTime = 8f;
    private Image image;
    private bool startWhitening = false;

    public void StartWhitening()
    {
        startWhitening=true;
        GameController.IsGameOver = true;
    }
    private void Start()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
        if (startWhitening)
        {
            image.color = new Color(0.7f, 0.7f, 0.7f, image.color.a + Time.deltaTime / WhiteTime);
            if (image.color.a >= 1)
            {
                GameController.EscapeSucceeded();
            }
        }
    }
}
