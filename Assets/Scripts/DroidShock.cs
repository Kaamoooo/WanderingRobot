using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroidShock : MonoBehaviour
{
    public bool IsShocked=false;

    public ParticleSystem ShockParticleSystem;
    public AudioSource ShockAudioSource;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void SetShock()
    {
        IsShocked = true;
        navMeshAgent.ResetPath();
        ShockAudioSource.Play();
        ShockParticleSystem.Play();
        Invoke("setShock", 6f);
    }
    private void setShock()
    {
        IsShocked = false;
    }
}
