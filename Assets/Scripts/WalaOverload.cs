using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalaOverload : MonoBehaviour
{
    public float PressTime = 2f;
    public GameObject SliderObject;
    public TextMeshProUGUI OverloadTip;
    public BatteryUI Battery;
    public ParticleSystem OverloadParticleSystem;
    public AudioSource OverloadAudioSource;
    
    private Slider slider;
    private float pressDownTime=0;
    private bool canOverload=false;
    private Coroutine SetOverloadTipCoroutine = null;
    private void Start()
    {
        slider = SliderObject.GetComponent<Slider>();
        slider.value = 0;
    }
    void Update()
    {
        if (GameController.IsGameOver||GameController.CurBattery==0) return;
        if (GameController.hasBeenCaptured)
        {
            if (!canOverload)
            {
                if(SetOverloadTipCoroutine==null)SetOverloadTipCoroutine=StartCoroutine(SetOverloadTip(true, 2f));
                return;
            }
            else
            {
                if (Input.GetButton("Overload"))
                {
                   pressDownTime += Time.deltaTime;
                    slider.value = pressDownTime / PressTime;
                }else
                {
                    pressDownTime -= Time.deltaTime;
                    if(pressDownTime < 0) pressDownTime = 0;
                    slider.value=pressDownTime / PressTime;
                }
                if (pressDownTime > PressTime)
                {
                    if(SetOverloadTipCoroutine==null)SetOverloadTipCoroutine=StartCoroutine(SetOverloadTip(false, 0f));

                    OverloadAudioSource.Play();
                    OverloadParticleSystem.Play();
                    Battery.Overload();
                    pressDownTime = 0;
                    GameController.hasBeenCaptured = false;
                    GameController.CaptureDroid.GetComponent<DroidShock>().SetShock();
                    gameObject.GetComponent<WalaMovement>().ResetStatus();
                }
            }

        }
     }

    private IEnumerator SetOverloadTip(bool setActive,float time)
    {   
        yield return new WaitForSeconds(time);
        if (SliderObject.activeSelf) { slider.value = 0; }
        SliderObject.SetActive(setActive);
        OverloadTip.enabled=setActive;
        canOverload = setActive;
        SetOverloadTipCoroutine = null;
    }
}
