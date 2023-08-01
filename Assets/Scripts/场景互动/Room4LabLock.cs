using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Room4LabLock : ObjectInteractive
{
    string TipMessage = "Press E to open the door.";
    public PlayableDirector PlayableDirector;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDoorOpenSound()
    {
        audioSource.Play();
    }
    public override bool Interact()
    {
        if (HasInteracted) { return false; }
        HasInteracted = true;
        PlayableDirector.Play();
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
