using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3Component : ObjectInteractive
{
    string TipMessage = "Press E to get antenna.";
    public Material ScanEffectMaterial;
    public ScanEffect ScanEffect;
    public override bool Interact()
    {
        if (HasInteracted) { return false; }
        HasInteracted = true;
        GameController.AddProp("antenna");
        StartCoroutine(InteractSuccessTip.SetInteractSuccessTip("You got an antenna. Your view seems to be more clear."));
        ScanEffectMaterial.SetFloat("_MaxVisualDistance", 8);
        ScanEffect.ScanSpeed = 9;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject,3f);
        return true;
    }

    new void OnTriggerEnter(Collider other)
    {
        base.SetTipMessage(TipMessage);
        base.OnTriggerEnter(other);
    }
}
