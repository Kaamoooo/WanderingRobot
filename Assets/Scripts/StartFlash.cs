using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFlash : MonoBehaviour
{
    public GameObject BatteryUI;
    public float FlashInterval=3f;
    public float InitialClippingPlane = 0.5f;
    public float NormalClippingPlane = 60f;

    private bool endLerp=false;
    private Camera _camera;
    private float curLerp=0.01f;
    void Start()
    {
        GameController.StartMoving = false;
        _camera= Camera.main;
        _camera.farClipPlane = InitialClippingPlane;
        BatteryUI.SetActive(false);
    }

    void Update()
    {
        if (endLerp) return;
        _camera.farClipPlane = Mathf.Lerp(InitialClippingPlane, NormalClippingPlane, curLerp);
        curLerp += Time.deltaTime / FlashInterval*Mathf.Sqrt((_camera.farClipPlane-InitialClippingPlane)/(NormalClippingPlane-InitialClippingPlane));
        if (_camera.farClipPlane > NormalClippingPlane-1)
        {
            GameController.StartMoving = true;
            endLerp = true;
            BatteryUI.SetActive(true);
        }
    }
}
