using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidReveal : MonoBehaviour
{
    public SkinnedMeshRenderer NormalRenderer;
    public SkinnedMeshRenderer InvisibleDroidRenderer;
    public float RevealSpeed = 0.05f;

    private Material[] normalMaterials;
    private Material[] invisibleMaterials;

    private MaterialPropertyBlock dissolveProperty;

    private bool isInvisible = true;
    void Start()
    {
        normalMaterials = NormalRenderer.materials;
        invisibleMaterials = InvisibleDroidRenderer.materials;
        dissolveProperty = new MaterialPropertyBlock();
        dissolveProperty.SetFloat("_DissolveAmount", 1);
        InvisibleDroidRenderer.SetPropertyBlock(dissolveProperty);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.hasBeenCaptured==true&&isInvisible) {
            Reveal();
            isInvisible = false;
        }
        else if (GameController.hasBeenCaptured == false&&!isInvisible) {
            Lurk();
            isInvisible = true;
        }
    }

    private void Reveal()
    {
        InvisibleDroidRenderer.materials = normalMaterials;
        InvisibleDroidRenderer.SetPropertyBlock(dissolveProperty);
        StartCoroutine(SetDissolveAmount(true));
    }
    IEnumerator SetDissolveAmount(bool reveal)
    {
        float dissolveAmount = reveal?1:0;
        int dir=reveal ? 1 : -1;
        while (reveal?dissolveAmount >=0: dissolveAmount <=1)
        {
            dissolveAmount -= RevealSpeed*dir;
            dissolveProperty.SetFloat("_DissolveAmount", dissolveAmount);
            InvisibleDroidRenderer.SetPropertyBlock(dissolveProperty);
            yield return new WaitForSeconds(0.1f);
        }
        if(!reveal) { 
              InvisibleDroidRenderer.materials = invisibleMaterials;
        }
    }

    private void Lurk()
    {
        StartCoroutine(SetDissolveAmount(false));
    }
}

