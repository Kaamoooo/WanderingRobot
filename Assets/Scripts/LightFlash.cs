using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlash : MonoBehaviour
{
    public float MaxLightInterval;
    public float ExtinctInterval;
    public Material PanelLightMaterial;

    private Coroutine flashCoroutine;

    void Update()
    {
        if (flashCoroutine == null)
        {
            flashCoroutine=StartCoroutine(Flash());
        }
    }
    IEnumerator Flash()
    {
        float interval=Random.Range(0, MaxLightInterval);
        yield return new WaitForSeconds(interval);
        PanelLightMaterial.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(ExtinctInterval);
        PanelLightMaterial.EnableKeyword("_EMISSION");
        flashCoroutine = null;
    }
}
