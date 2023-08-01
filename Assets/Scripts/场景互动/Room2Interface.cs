using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2Interface : ObjectInteractive
{
    string TipMessage = "Press E to read password.";
    public override bool Interact()
    {
        if(HasInteracted) { return false; }
        HasInteracted = true;
        GameController.AddProp("Room3Password");
        StartCoroutine(InteractSuccessTip.SetInteractSuccessTip("You just got a password for a jail cell."));
        return true;
    }

    new void OnTriggerEnter(Collider other)
    {
        base.SetTipMessage(TipMessage);
        base.OnTriggerEnter(other);
    }
}
