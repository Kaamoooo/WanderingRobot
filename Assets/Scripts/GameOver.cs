using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public float EscapeFailedTextAppearTime = 3f;
    public float EscapeSucceededTextAppearTime = 6f;
    public GameObject EscapeFailedTextObj;
    public GameObject EscapeSucceededTextObj;
    public GameObject RestartBtn;
    public WalaSound WalaSound;
    public GameObject BGM;

    private TextMeshProUGUI EscapeFailedText;
    private TextMeshProUGUI EscapeSucceededText;
    private bool isFailed=false;
    private bool isSucceeded=false;

    private void Start()
    {
        EscapeSucceededText=EscapeSucceededTextObj.GetComponent<TextMeshProUGUI>();
        EscapeFailedText = EscapeFailedTextObj.GetComponent<TextMeshProUGUI>();
        BGM.SetActive(true);
    }

    public void EscapeFailed()
    {
        if (!isFailed&&!isSucceeded)WalaSound.PlayEscapeFailedSound();
        isFailed = true;
    }
    public void EscapeSucceeded()
    {
        if (!isSucceeded && !isFailed)WalaSound.PlayEscapeSucceedSound();
        isSucceeded = true;
        BGM.SetActive(false);
    }
    void Update()
    {
        if (isFailed&&!isSucceeded)
        {
            if(EscapeFailedTextObj.activeSelf==false)EscapeFailedTextObj.SetActive(true);
            EscapeFailedText.color = new Color(1, 0, 0, EscapeFailedText.color.a + Time.deltaTime / EscapeFailedTextAppearTime);
            if (EscapeFailedText.color.a >= 1)
            {
                RestartBtn.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
        if (isSucceeded&&!isFailed)
        {
            if (EscapeSucceededTextObj.activeSelf == false) EscapeSucceededTextObj.SetActive(true);
            EscapeSucceededText.color = new Color(0,0.9f , 0, EscapeSucceededText.color.a + Time.deltaTime / EscapeSucceededTextAppearTime);
            if (EscapeSucceededText.color.a >= 1)
            {
                RestartBtn.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }


    public void Restart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameController.Reset();
        SceneManager.LoadScene("MainScene",LoadSceneMode.Single);
    }
}
