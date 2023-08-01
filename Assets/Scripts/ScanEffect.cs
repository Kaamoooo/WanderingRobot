using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanEffect : MonoBehaviour
{
    public float ScanSpeed = 5.0f;
    public float MaxScanDistance = 30f;
    public Material ScanMaterial;
    public float MaxScanBrightness=0.5f;
    public Transform ScanOrigin;
    public float MaxVisualDistance = 2;
    public TurnOnLight TurnOnLight;

    private float _curScanDistance=100f;
    private Coroutine scanCoroutine;
    private bool isCaptured=false;
    
    private float startVisualDistance = 40f;
    private bool startScan=false;
    private Coroutine initialScanCoroutine;

    public AudioClip[] AudioClips;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
        ScanMaterial.SetFloat("_MaxVisualDistance", startVisualDistance);
    }

    void Update()
    {
        InitialScan();
        if (!startScan)
        {
            return;
        }
        if (GameController.IsGameOver) { return; }
        if(GameController.hasBeenCaptured&&!isCaptured)
        {
            isCaptured=true;
            _curScanDistance = 0;
            ScanMaterial.SetFloat("_MaxVisualDistance", MaxVisualDistance+2);
            return;
        }
        if (!GameController.hasBeenCaptured&&isCaptured)
        {
            ScanMaterial.SetFloat("_MaxVisualDistance", MaxVisualDistance);
            isCaptured=false;
        }
        if(isCaptured)
        {
            _curScanDistance = 0;
            return;
        }
        _curScanDistance += Time.deltaTime * ScanSpeed;
        if (_curScanDistance > MaxScanDistance)
        {
            if (scanCoroutine == null) {
                scanCoroutine = StartCoroutine(ResetScanDistance(1f));
            }
        }
    }

    private void InitialScan()
    {
        if (TurnOnLight.DarkenVision && !startScan)
        {
            if(initialScanCoroutine == null)
            {
                initialScanCoroutine =  StartCoroutine(InitialScanDistanceCoroutine());
            }
        }
        
    }
    IEnumerator InitialScanDistanceCoroutine()
    {
        while (startVisualDistance > MaxVisualDistance)
        {
            startVisualDistance -= 1.4f;
            ScanMaterial.SetFloat("_MaxVisualDistance", startVisualDistance);
            yield return new WaitForSeconds(0.1f);
        }
        startScan = true;

    }
    IEnumerator ResetScanDistance(float cd)
    {
        yield return new WaitForSeconds(cd);
        _curScanDistance = MaxVisualDistance;
        scanCoroutine = null;
        audioSource.PlayOneShot(AudioClips[0]);
    }
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        ScanMaterial.SetFloat("_ScanDistance", _curScanDistance);
        ScanMaterial.SetVector("_WorldScanOrigin", ScanOrigin.position);
        Graphics.Blit(source, destination, ScanMaterial);
    }
}
