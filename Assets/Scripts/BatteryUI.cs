using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    public float CurrentBatteryVolume=4;
    public Image BatteryUIImage;
    public Sprite Battery4;
    public Sprite Battery3;
    public Sprite Battery2;
    public Sprite Battery1;
    public Sprite Battery0; 

    public void Overload()
    {
        GameController.CurBattery=--CurrentBatteryVolume;
        switch(CurrentBatteryVolume) { 
            case 4: 
                BatteryUIImage.sprite = Battery4;
                break;
            case 3:
                BatteryUIImage.sprite = Battery3;
                break;
            case 2:
                BatteryUIImage.sprite = Battery2;
                break;
            case 1:
                BatteryUIImage.sprite = Battery1;
                break;
            case 0:
                BatteryUIImage.sprite = Battery0;
                break;
        }
    }

}
