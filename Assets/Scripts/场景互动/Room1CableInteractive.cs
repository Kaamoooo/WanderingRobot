using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1CableInteractive : ObjectInteractive
{
    public string TipMessage1 = "Need Arm";
    public string TipMessage2 = "Press E to short-circuit the cable";

    public MeshRenderer LockMeshRender;
    public Material LockGreenMaterial;
    public AudioSource DoorAudioSource;
    public AudioSource CableAudioSource;
    public Animator DoorAnimator;

    public ParticleSystem ParticleSystem;
    public ParticleSystem OriginalParticleSystem;

    private bool _hasBeenShortCircuited=false;

    private void Start()
    {
    }

    new void OnTriggerEnter(Collider other)
    {
        if (_hasBeenShortCircuited) return;
        if (GameController.HasProp("Arm"))
        {
            SetTipMessage(TipMessage2);
        }
        else
        {
            SetTipMessage(TipMessage1);
        }
        base.OnTriggerEnter(other);
    }
    public override bool Interact()
    {
        if(HasInteracted) { return false; }
        if (GameController.HasProp("Arm"))
        {
            StartCoroutine(InteractSuccessTip.SetInteractSuccessTip("You have short-circuited the cable. The door seems to be opened."));

            GameController.StartPersuading = true;

            _hasBeenShortCircuited= true;
            ParticleSystem.Play();
            OriginalParticleSystem.Stop();
            CableAudioSource.Play();
            DoorAudioSource.Play();
            GameController.UseProp("Arm");
            Material[] materials = LockMeshRender.materials;
            materials[1] = LockGreenMaterial;
            LockMeshRender.materials = materials;
            DoorAnimator.SetTrigger("DoorOpen");

            return true;
        }
        else
        {
            return false;
        }
    }

}
