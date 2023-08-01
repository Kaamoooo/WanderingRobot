using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3DoorLock : ObjectInteractive
{
    public Animator DoorAnimator;
    public Material DoorLockMaterial;
    public AudioSource DoorAudioSource;

    string TipMessage1 = "Press E to open the door.";
    string TipMessage2 = "Need Password.";
    public override bool Interact()
    {
        if(HasInteracted||!GameController.HasProp("Room3Password")) { return false; }
        HasInteracted = true;
        DoorAnimator.SetTrigger("DoorOpen");
        DoorAudioSource.Play();
        Material[] materials=GetComponent<MeshRenderer>().materials;
        materials[1]=DoorLockMaterial;
        GetComponent<MeshRenderer>().materials=materials;
        return true;
    }
    new void OnTriggerEnter(Collider other)
    {
        if (HasInteracted) return;
        if (other.gameObject.CompareTag("MainCamera"))
        {
            if(GameController.HasProp("Room3Password")) SetTipMessage(TipMessage1);
            else SetTipMessage(TipMessage2);
        }
        base.OnTriggerEnter(other);

    }

}
