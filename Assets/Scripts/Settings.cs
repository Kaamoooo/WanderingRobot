using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool IsSettingsOpen=false;
    public GameObject BackgroundImage;
    public GameObject Buttons;
    public Slider MouseSensitivitySlider;
    public Slider VolumeSlider;
    public WalaMovement WalaMovement;

    private static float Volume = 1f;
    private static float MaxMouseSensitivity = 120f;

    private void Start()
    {
        VolumeSlider.value = Volume;
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            IsSettingsOpen = !IsSettingsOpen;
            if(IsSettingsOpen)
            {
                OpenSettings();
            }
            else
            {
                CloseSettings();
            }
        }
        if (WalaMovement!=null&&WalaMovement.MouseSensitivity != MouseSensitivitySlider.value * 45)
        {
            MaxMouseSensitivity = MouseSensitivitySlider.value * 45;
            WalaMovement.MouseSensitivity = MaxMouseSensitivity;
        }
        Volume=VolumeSlider.value;
        AudioListener.volume = Volume;
    }

    public void Resume()
    {
        CloseSettings();
    }
    public void QuitToMenu()
    {
        CloseSettings();
        Cursor.lockState = CursorLockMode.None;
        GameController.Reset();
        SceneManager.LoadScene(0);
    }
    public void QuitToDesktop()
    {
        CloseSettings();
        Application.Quit();
    }

    public void CloseSettings()
    {
        AudioListener.pause = false;
        IsSettingsOpen = false;
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        BackgroundImage.SetActive(false);
        Buttons.SetActive(false);
    }
    public void OpenSettings()
    {
        AudioListener.pause = true;
        IsSettingsOpen=true;
        Time.timeScale= 0.0f;
        Cursor.lockState=CursorLockMode.None;
        BackgroundImage.SetActive(true);
        Buttons.SetActive(true);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
