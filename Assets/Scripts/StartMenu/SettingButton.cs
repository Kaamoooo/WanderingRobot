using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public Settings Settings;
    public void OnClick()
    {
        Settings.IsSettingsOpen=!Settings.IsSettingsOpen; ;
        if (Settings.IsSettingsOpen)
        {
            Settings.OpenSettings();
        }
        else
        {
            Settings.CloseSettings();
        }
    }
    private void Update()
    {
            Cursor.lockState = CursorLockMode.None;
    }
}
