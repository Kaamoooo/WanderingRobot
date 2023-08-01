using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Room4Interface : ObjectInteractive
{
    string TipMessage = "Press E to hack the computer.";
    string TipMessage2 = "You've hacked in the computer. Leave there right now!";
    public override bool Interact()
    {
        if (HasInteracted) { return false; }
        HasInteracted = true;
        GameController.AddProp("ExitPassword");
        StartCoroutine(InteractSuccessTip.SetInteractSuccessTip(TipMessage2));
        return true;
    }
    new void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            base.SetTipMessage(TipMessage);
        }
        base.OnTriggerEnter(other);

    }
}
