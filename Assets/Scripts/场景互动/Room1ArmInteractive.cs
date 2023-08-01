using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Room1ArmInteractive : ObjectInteractive
{
    public string Name = "Arm";
    string TipMessage = "Press E to pick up arm.";

    new void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            base.SetTipMessage(TipMessage);
        }
        base.OnTriggerEnter(other);
       
    }


    public override bool Interact()
    {
        base.HasInteracted= true;
        GameController.Props.Add(Name);
        StartCoroutine(InteractSuccessTip.SetInteractSuccessTip("You got the " + Name + "."));
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject,3f);
        return true;
    }

}
