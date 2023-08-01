using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroidMovement : MonoBehaviour
{
    public GameObject Wala;
    public float MinProbeDistance;
    public float MinLockDistance;
    public Transform[] Coords;
    public WalaSleep WalaSleep;
    public DroidPatrolSector DroidPatrolSector;

    private float droidWalaDistance=>Vector3.Distance(transform.position,walaTransform.position);

    private bool isShocked { get => droidShock.IsShocked; set => isShocked = value; }
    private DroidShock droidShock;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Transform walaTransform;
    private WalaMovement walaMovement;
    public bool IsCapturing=false;
    private bool isPersuading=false;
    private int currentCoord=0;
    

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        walaTransform = Wala.GetComponent<Transform>();
        walaMovement = Wala.GetComponent<WalaMovement>();
        droidShock = GetComponent<DroidShock>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPersuading) { Patrol(); }

        Probe();
        SetAnimationState();
    }
    private void SetAnimationState()
    {

        if (navMeshAgent.velocity.magnitude > 0.5f)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
        if (isPersuading && droidWalaDistance < 3f && !IsCapturing)
        {
            animator.SetBool("IsCapturing", true);
            IsCapturing = true;
        }
        else if (droidWalaDistance > 3f && IsCapturing)
        {
            animator.SetBool("IsCapturing", false); IsCapturing = false;
        }
    }
    private void Probe()
    {
        if(isShocked) { 
            isPersuading = false;
            return; 
        }
        if ( GameController.StartPersuading &&
            ((droidWalaDistance > MinLockDistance && droidWalaDistance <= MinProbeDistance && !WalaSleep.IsSleeping )
            || droidWalaDistance < MinLockDistance))
        {
            if(!GameController.hasBeenCaptured&&DroidPatrolSector.IsWalaInSector)navMeshAgent.SetDestination(walaTransform.position);
            
            isPersuading = true;
            if (!DroidPatrolSector.IsWalaInSector)
            {
                navMeshAgent.ResetPath();
                return;
            }
            if (!navMeshAgent.pathPending&&navMeshAgent.remainingDistance < 1.5f&&!isShocked &&!GameController.hasBeenCaptured)
           {
                navMeshAgent.ResetPath();
                GameController.hasBeenCaptured = true;
                GameController.CaptureDroid = this.gameObject;
                Vector3 relativeVector=walaTransform.position-transform.position;
                transform.rotation=Quaternion.LookRotation(new Vector3(relativeVector.x,0,relativeVector.z));
            }
        }
        else
        {
            isPersuading = false;
        }
    }
    private void Patrol()
    {
        if(isShocked) { return; }
        navMeshAgent.SetDestination(Coords[currentCoord].position);
        if(navMeshAgent.remainingDistance < 1&&!navMeshAgent.pathPending)
        {
            currentCoord++;
            if(currentCoord>=Coords.Length)
            {
                currentCoord = 0;
            }
        }
    }
    
}


