using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnLight : MonoBehaviour
{
    public Light[] Lights;
    public float LightingInterval = 1f;
    public Material NoticeBoardBackground;

    public bool DarkenVision=false;
    public GameObject Droid0;

    private Coroutine ProgressCoroutine;

    private void Start()
    {
        NoticeBoardBackground.SetColor("_EmissionColor", new Color(22, 22, 22)/255);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ProgressCoroutine==null)
            {
                ProgressCoroutine= StartCoroutine(StartProgress());
            }
        }
    }

    private IEnumerator StartProgress()
    {

        for (int i = 0;i < Lights.Length;i++)
        {
            Lights[i].intensity = 0.5f;
            Lights[i].GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(LightingInterval);
        }

        NoticeBoardBackground.SetColor("_EmissionColor", new Color(128, 68, 68) / 255);
        yield return new WaitForSeconds(0.2f);
        NoticeBoardBackground.SetColor("_EmissionColor", new Color(68, 47, 47)/255);
        yield return new WaitForSeconds(0.4f);
        NoticeBoardBackground.SetColor("_EmissionColor", new Color(150,100,100) / 255);
        yield return new WaitForSeconds(0.1f);
        NoticeBoardBackground.SetColor("_EmissionColor", new Color(88,47,47) / 255);
        yield return new WaitForSeconds(0.7f);
        NoticeBoardBackground.SetColor("_EmissionColor", new Color(255,128,128) / 255);

        yield return new WaitForSeconds(0.5f);
        DarkenVision = true;
        Droid0.SetActive(true);
        GameController.WalaSound.PlayVisionDecrease();
    }
}
